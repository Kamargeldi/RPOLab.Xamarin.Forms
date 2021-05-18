using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using RPOLab.Models;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Specialized;
using Firebase.Storage;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;


namespace RPOLab.FireBaseDB
{
    public class FireBaseService
    {
        FirebaseClient client;
        FirebaseStorage storage;
        public FireBaseService()
        {
            var config = $"https://videoandroid-271121-44924-default-rtdb.firebaseio.com/";
            client = new FirebaseClient(config);

            storage = new FirebaseStorage($"videoandroid-271121-44924.appspot.com");
        }
        public List<Film> GetFilms()
        {
            var filmsRaw = client.Child("films")
                .OnceAsync<Film>().Result.Select(x =>
                {
                    var film = x.Object;
                    film.Key = x.Key;
                    return film;
                }).ToList();

            return filmsRaw;
        }
        public async Task AddFilm(Film film)
        {
            await client.Child("films")
                .PostAsync(JsonConvert.SerializeObject(film));
        }
        public async Task<string> UploadImage(FileData file)
        {


            if (file != null)
            {
                Guid guid = Guid.NewGuid();
                await storage.Child("images")
                    .Child(guid.ToString() + Path.GetExtension(file.FileName))
                    .PutAsync(new MemoryStream(file.DataArray));

                var url =  await storage.Child("images")
                    .Child(guid.ToString() + Path.GetExtension(file.FileName))
                    .GetDownloadUrlAsync();

                return url;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> UploadVideo(FileData file)
        {


            if (file != null)
            {
                Guid guid = Guid.NewGuid();
                await storage.Child("videos")
                    .Child(guid.ToString() + Path.GetExtension(file.FileName))
                    .PutAsync(new MemoryStream(file.DataArray));

                var url = await storage.Child("videos")
                    .Child(guid.ToString() + Path.GetExtension(file.FileName))
                    .GetDownloadUrlAsync();

                return url;
            }
            else
            {
                return null;
            }
        }

        public async Task<FileData> PickImage()
        {
            var fileData =  await CrossFilePicker.Current.PickFile();
            return fileData;
        }

        public async Task<FileData> PickVideo()
        {
            var fileData = await CrossFilePicker.Current.PickFile();
            return fileData;
        }
        
        public List<string> GetFontNames()
        {
            return client.Child("fontNames")
                .OnceAsync<string>().Result.Select(x => x.Object).ToList();
        }
        public List<int> GetRatings()
        {
            return client.Child("ratings")
                .OnceAsync<int>().Result.Select(x => x.Object).ToList();
        }
        public List<string> GetLanguages()
        {
            return client.Child("languages")
                .OnceAsync<string>().Result.Select(x => x.Object).ToList();
        }
        public Settings GetSettings()
        {
            var setting = client.Child("settingsState")
                .OnceSingleAsync<Settings>().Result;
            
            return setting;
        }
        public Filter GetFilters()
        {
            var filters = client.Child("filterState")
                .OnceSingleAsync<Filter>().Result;

            return filters;
        }
        public async Task SaveSettings(Settings settings)
        {
            await client.Child("settingsState").PutAsync(JsonConvert.SerializeObject(settings));
        }
        
        public async Task SaveFilters(Filter filters)
        {
            await client.Child("filterState").PutAsync(JsonConvert.SerializeObject(filters));
        }

        public List<Film> GetFilteredFilms(Filter filter)
        {
            IEnumerable<Film> films = GetFilms();
            if (filter.Name != "")
                films = films.Where(x => x.Name == filter.Name);
            if (filter.Producer != "")
                films = films.Where(x => x.Producer == filter.Producer);
            if (filter.Year != 0)
                films = films.Where(x => x.Year == filter.Year);
            if (filter.Rating != 0)
                films = films.Where(x => x.Rating == filter.Rating);

            films = films.Where(x => x.HasImage == filter.HasImage);
            films = films.Where(x => x.HasVideo == filter.HasVideo);

            return films.ToList();
        }

        public (List<string> producers, List<int> ratings, List<string> names, List<int> years) GetAvailableFilterValues()
        {
            var films = GetFilms();
            var producers = films.Select(x => x.Producer).Where(x => x != null).Distinct().ToList();
            var ratings = films.Select(x => x.Rating).Where(x => x != 0).Distinct().ToList();
            var names = films.Select(x => x.Name).Where(x => x != null).Distinct().ToList();
            var years = films.Select(x => x.Year).Where(x => x != 0).Distinct().ToList();
            return (producers, ratings, names, years);
        }
    }
}
