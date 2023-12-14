using ConsoleTools;
using ISTSU0_ADT_2023241.Client;
using ISTSU0_ADT_2023241.Endpoint.Dtos;
using ISTSU0_ADT_2023241.Models;
using Newtonsoft.Json;
using System.Xml.Linq;

internal class Program
{
    static RestService restService = new RestService("http://localhost:5082");
    private static void Main(string[] args)
    {
        var bandSubMenu = new ConsoleMenu(args, level: 1)
        .Add("DoesThisBandHaveMultipleGuitarists", () => DoesThisBandHaveMultipleGuitarists())
        .Add("WhatGuitarsDoesThisBandHave", () => WhatGuitarsDoesThisBandHave())
        .Add("GetAll", () => GetAllBands())
        .Add("GetOne", () => GetOneBand())
        .Add("Delete", () => DeleteBand())
        .Add("Create", () => CreateBand())
        .Add("Update", () => UpdateBand())
        .Add("Back", ConsoleMenu.Close)
        .Configure(config =>
        {
            config.Selector = "--> ";
            config.EnableFilter = true;
            config.Title = "Band";
            config.EnableBreadcrumb = true;
            config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
        });

        var guitarSubMenu = new ConsoleMenu(args, level: 1)
        .Add("GetAll", () => GetAllGuitars())
        .Add("GetOne", () => GetOneGuitar())
        .Add("Delete", () => DeleteGuitar())
        .Add("Create", () => CreateGuitar())
        .Add("Update", () => UpdateGuitar())
        .Add("Back", ConsoleMenu.Close)
        .Configure(config =>
        {
            config.Selector = "--> ";
            config.EnableFilter = true;
            config.Title = "Guitar";
            config.EnableBreadcrumb = true;
            config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
        });

        var guitaristSubMenu = new ConsoleMenu(args, level: 1)
        .Add("WhereDoesThisGuitaristPlay", () => WhereDoesThisGuitaristPlay())
        .Add("GetAll", () => GetAllGuitarists())
        .Add("GetOne", () => GetOneGuitarists())
        .Add("Delete", () => DeleteGuitarist())
        .Add("Create", () => CreateGuitarist())
        .Add("Update", () => UpdateGuitarist())
        .Add("Back", ConsoleMenu.Close)
        .Configure(config =>
        {
            config.Selector = "--> ";
            config.EnableFilter = true;
            config.Title = "Guitarist";
            config.EnableBreadcrumb = true;
            config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
        });


        new ConsoleMenu(args, level:0)
                .Add("Band", bandSubMenu.Show)
                .Add("Guitar", guitarSubMenu.Show)
                .Add("Guitarist", guitaristSubMenu.Show)
                .Add("Close", ConsoleMenu.Close)
                .Configure(config => { config.Selector = "--> "; })
                .Show();
    }

    private static void UpdateGuitarist()
    {
        Console.WriteLine("Give guitarist Id");
        string id = Console.ReadLine();
        Console.WriteLine("Give band id");
        string bandId = Console.ReadLine();
        Console.WriteLine("Give the age");
        int age = int.Parse(Console.ReadLine());
        var result = restService.Put<Guitarist, UpdateGuitaristDto>(new UpdateGuitaristDto { BandId = Guid.Parse(bandId), Age=age }, $"/api/Guitarist/Update/{id}");
        if (result != null)
        {
            Console.WriteLine("Guitarist updated");
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            Console.ReadKey();
        }
    }

    private static void CreateGuitarist()
    {
        Console.WriteLine("Give a name");
        string name = Console.ReadLine();
        Console.WriteLine("Give the age");
        int age = int.Parse(Console.ReadLine());
        Console.WriteLine("Give the bandId");
        string id= Console.ReadLine();
        var result = restService.Post<Guitarist, CreateGuitaristDto>(new CreateGuitaristDto { Age = age, Name = name, BandId=Guid.Parse(id) }, $"/api/Guitarist/Create");
        if (result != null)
        {
        Console.WriteLine("Guitarist added");
        Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        Console.ReadKey();
        }

    }

    private static void DeleteGuitarist()
    {
        Console.WriteLine("Give a GuitaristID");
        string input = Console.ReadLine();
        restService.Delete(input, $"/api/Guitarist/Delete");
        Console.WriteLine("");
        Console.WriteLine("Guitarist deleted");
        Console.ReadKey();
    }

    private static void GetOneGuitarists()
    {
        Console.WriteLine("Give a GuitaristID");
        string input = Console.ReadLine();
        var result = restService.GetSingle<Guitarist>($"/api/Guitarist/GetOne/{input}");
        Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        Console.ReadKey();
    }

    private static void GetAllGuitarists()
    {
        var result = restService.Get<Guitarist>($"/api/Guitarist/GetAll");
        Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        Console.ReadKey();
    }

    private static void UpdateGuitar()
    {
        Console.WriteLine("Give guitar Id");
        string id = Console.ReadLine();
        Console.WriteLine("Give new guitarist id");
        string guitaristId= Console.ReadLine();
        var result = restService.Put<Guitar, UpdateGuitarDto>(new UpdateGuitarDto { GuitaristId = Guid.Parse(guitaristId) }, $"/api/Guitar/Update/{id}");
        if (result != null)
        {
            Console.WriteLine("Guitar updated");
            Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
            Console.ReadKey();
        }
    }

    private static void CreateGuitar()
    {
        Console.WriteLine("Give a brand");
        string brand = Console.ReadLine();
        Console.WriteLine("Give a model");
        string model = Console.ReadLine();
        Console.WriteLine("Give a Body Type");
        string bodyTypeString = Console.ReadLine();
        Console.WriteLine("Give a GuitaristId");
        Guid guitaristId = Guid.Parse(Console.ReadLine());
        if (Enum.TryParse<BodyType>(bodyTypeString, out var bodyType))
        {
            var result = restService.Post<Guitar, CreateGuitarDto>(new CreateGuitarDto { Model = model, Brand = brand,BodyType= bodyType,GuitaristId=guitaristId }, $"/api/Guitar/Create");
            if (result != null)
            {
                Console.WriteLine("Guitar added");
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("give a valid BodyType");
            CreateBand();
        }
    }

    private static void DeleteGuitar()
    {
        Console.WriteLine("Give a GuitarID");
        string input = Console.ReadLine();
        restService.Delete(input, $"/api/Guitar/Delete");
        Console.WriteLine("");
        Console.WriteLine("Guitar deleted");
        Console.ReadKey();
    }

    private static void GetOneGuitar()
    {
        Console.WriteLine("Give a GuitarID");
        string input = Console.ReadLine();
        var result = restService.GetSingle<Guitar>($"/api/Guitar/GetOne/{input}");
        Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        Console.ReadKey();
    }

    private static void GetAllGuitars()
    {
        var result = restService.Get<Guitar>($"/api/Guitar/GetAll");
        Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        Console.ReadKey();
    }

    private static void WhereDoesThisGuitaristPlay()
    {
        Console.WriteLine("Give a GuitaristID");
        string input = Console.ReadLine();
        var result = restService.GetSingle<Band>($"/api/Guitarist/WhereDoesThisGuitaristPlay/{input}");
        Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
        Console.ReadKey();
    }

    private static void UpdateBand()
    {
        Console.WriteLine("Give band Id");
        string id = Console.ReadLine();
        Console.WriteLine("Give a band name");
        string name = Console.ReadLine();
        Console.WriteLine("Give a genre");
        string genreString = Console.ReadLine();
        if (Enum.TryParse<Genre>(genreString, out var genre))
        {
            var result = restService.Put<Band, UpdateBandDto>(new UpdateBandDto { Genre = genre, Name = name }, $"/api/Band/Update/{id}");
            if (result!=null)
            {
                Console.WriteLine("Band updated");
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("give a valid genre");
            UpdateBand();
        }
        
    }

    private static void CreateBand()
    {
        Console.WriteLine("Give a band name");
        string name = Console.ReadLine();
        Console.WriteLine("Give a genre");
        string genreString = Console.ReadLine();
        if (Enum.TryParse<Genre>(genreString, out var genre))
        {
            var result=restService.Post<Band,CreateBandDto> (new CreateBandDto { Genre=genre,Name=name}, $"/api/Band/Create");
            if (result!=null)
            {
                Console.WriteLine("Band added");
                Console.WriteLine(JsonConvert.SerializeObject(result, Formatting.Indented));
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("give a valid genre");
            CreateBand();
        }

    }

    private static void DeleteBand()
    {
        Console.WriteLine("Give a BandID");
        string input = Console.ReadLine();
        restService.Delete(input,$"/api/Band/Delete");
        Console.WriteLine("");
        Console.WriteLine("Band deleted");
        Console.ReadKey();
    }

    private static void GetOneBand()
    {
        Console.WriteLine("Give a BandID");
        string input = Console.ReadLine();
        var result = restService.GetSingle<Band>($"/api/Band/GetOne/{input}");
        Console.WriteLine(JsonConvert.SerializeObject(result,Formatting.Indented));
        Console.ReadKey();
    }

    private static void GetAllBands()
    {
        var result = restService.Get<Band>($"/api/Band/GetAll");
        Console.WriteLine(JsonConvert.SerializeObject(result,Formatting.Indented));
        Console.ReadKey();
    }

    private static void WhatGuitarsDoesThisBandHave()
    {
        Console.WriteLine("Give a BandID");
        string input = Console.ReadLine();
        var result = restService.GetSingle<List <string>>($"/api/Band/WhatGuitarsDoesThisBandHave/{input}");
        foreach (var item in result)
        {
            Console.WriteLine(item);
        }
        Console.ReadKey();
    }

    private static void DoesThisBandHaveMultipleGuitarists()
    {
        Console.WriteLine("Give a BandID");
        string input = Console.ReadLine();
        var result = restService.GetSingle<bool>($"/api/Band/DoesThisBandHaveMultipleGuitarists/{input}");
        Console.WriteLine(result);
        Console.ReadKey();
    }

    private static void SomeAction(string v)
    {
        throw new NotImplementedException();
    }
}