using EFCoreCRUD.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EFCoreCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //var google = GetWebSiteSourceCodeAsync("https://www.google.com.tr/");
            //Console.WriteLine(google);

            //InsertUrl("https://www.google.com.tr/");
            //UpdateUrl("https://www.google.com.tr/", "https://www.google2.com.tr/");
            //DeleteUrl("https://www.google2.com.tr/");

            //Console.WriteLine(GetUrl());

            //var a = GetUrl();
            //Console.WriteLine(a.Id + " " + a.Url);
            //foreach (var val in a.SubUrlses)
            //{
            //    Console.WriteLine(val.Url);
            //}

           var t = GetUrlByName("https://www.google.com.tr/");
           var a = 10;


            Console.ReadKey();
        }

        static async Task<string> GetWebSiteSourceCodeAsync(string webSiteUrl)
        {
            //Async hale getirdim.
            using (var httpClient = new HttpClient())
            {
                var source = await httpClient.GetAsync(webSiteUrl);
                var htmlSource =await source.Content.ReadAsByteArrayAsync();
                return System.Text.Encoding.UTF8.GetString(htmlSource);
            }
        }

        private static List<string> DumpHRefs(string inputString)
        {
            Match m;
            string HRefPattern = @"href\s*=\s*(?:[""'](?<1>[^""']*)[""']|(?<1>\S+))";
            var t = new List<string>();
            try
            {
                m = Regex.Match(inputString, HRefPattern,
                    RegexOptions.IgnoreCase | RegexOptions.Compiled,
                    TimeSpan.FromSeconds(1));
                while (m.Success)
                {
                    t.Add(Convert.ToString(m.Groups[1]));

                    m = m.NextMatch();
                }
            }
            catch (Exception e)
            {

            }

            return t;

        }
        //connected and disconnected insertion
        static async void InsertUrl(string webSiteUrl)
        {
            var sourceCode =await GetWebSiteSourceCodeAsync(webSiteUrl);

            var list = DumpHRefs(sourceCode);

            var subUrls = new List<SubUrls>();
            for(int i =0; i<list.Count; i++)
            {
                subUrls.Add(new SubUrls()
                {
                    Url= list[i]
                });
            }


            using (var context = new UrlContext())
            {
                await context.Urls.AddAsync(new Urls()
                {
                    SubUrlses = subUrls,
                    SourceCode = sourceCode,
                    Url = webSiteUrl,
                }); 
                await context.SaveChangesAsync();
            }
        }

        static async void UpdateUrl(string webSiteUrl, string newWebSiteUrl)
        {
            using (var context = new UrlContext())
            {
                //Refactor firstordefault'ı async hale getirdim.
                var record =await context.Urls.FirstOrDefaultAsync(x => x.Url == webSiteUrl);
                //Update'i model state üzerinde kullandım (ChangeTracker)
                var entries = context.ChangeTracker.Entries();
                foreach( var entry in entries)
                {
                    record.Url = newWebSiteUrl;
                   
                }
                //SaveChanges'ı async hale getirdim
                await context.SaveChangesAsync();
            }

        }
        //first, single, firstordefault,  singledefault,find, any, where , select, join, exact, intersect, foreach, 
        static async void DeleteUrl(string webSiteUrl)
        {
            using (var context = new UrlContext())
            {
                //Firstordefault'ı async hale getirdim.
                var record =await context.Urls.FirstOrDefaultAsync(x => x.Url == webSiteUrl);
                context.Urls.Remove(record);

                //SaveChanges'ı async hale getirdim.
                await context.SaveChangesAsync();
            }
        }

        static async Task<Urls> GetUrl()
        {
            //Joinleme yaptım.
            using (var context= new UrlContext())
            {
                var source = from p in context.Urls
                             join v in context.SubUrls
                                 on p.Id equals v.UrlId
                             select new Urls()
                             {
                                 Id = p.Id,
                                 Url = p.Url,
                                 SubUrlses = p.SubUrlses,
           
                             };

                var a = await source.FirstOrDefaultAsync();
                return a;
            }
        }

        static async Task<Urls> GetUrlByName(string websiteUrl)
        {
            using (var context = new UrlContext())
            {
                //Refactor eager ve lazy loading arasında bulunan farklar
                return await context.Urls.Include(v => v.SubUrlses).FirstOrDefaultAsync(x => x.Url == websiteUrl);
            }
        }
    }
}




