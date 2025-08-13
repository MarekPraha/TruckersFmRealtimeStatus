using SocketIO.Core;
using SocketIOClient;
using System.Net.Http.Json;
using Newtonsoft.Json;
using TruckersFmRealtime.Models.Songs;
using TruckersFmRealtime.Models.Show;
using TruckersFmRealtime.ModelsDB;

namespace TruckersFmRealtime
{
    internal class Program
    {
        private static HttpClient httpClient;
        private static TruckerFmContext db;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            httpClient = new HttpClient();
            db = new TruckerFmContext();

            var client = new SocketIOClient.SocketIO("https://public-socket.truckers.fm/");

            client.On("song", ClientOnSongRecieved);

            client.OnAny(OnResponseReceived);
            client.Options.EIO = EngineIO.V3;
            client.OnError += CLientOnError;

            await client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static void CLientOnError(object? sender, string e)
        {
            Console.WriteLine("Encountered error:\n\t " + e);
        }

        private static async void ClientOnSongRecieved(SocketIOResponse response)
        {
            Console.WriteLine();
            Console.WriteLine("Recieved song");
            string responseString = response.ToString();

            var songsSocket = JsonConvert.DeserializeObject<List<SongSocketData>>(responseString);
            if (songsSocket.Count == 0)
            {
                Console.WriteLine("did not parse json/no songs got");
                return;
            }
            SongSocketData data = songsSocket[0];

            Console.WriteLine("From:\t\t" + data.current_song.song.artist);
            Console.WriteLine("Playing:\t" + data.current_song.song.title);

            var showResponse = await httpClient.GetStringAsync("https://radiocloud.pro/api/public/v1/presenter/live");
            ShowRoot showRoot = JsonConvert.DeserializeObject<ShowRoot>(showResponse);
            if (showRoot.status != "success")
            {
                Console.WriteLine("show did not return success");
                Console.WriteLine("\t" + showRoot.status);
                return;

            }
            Console.WriteLine("current show: " + showRoot.data.description);

            AddShow(showRoot.data);
            AddSong(data.current_song.song, showRoot.data);

        }

        private static void AddSong(SongFull song, DataShow show)
        {
            var songDb = db.TbSongs.Where(x => x.Title == song.title && x.Artist == song.artist).FirstOrDefault();
            if (songDb == null)
            {
                songDb = new TbSong()
                {
                    Title = song.title,
                    Artist = song.artist,
                    Link = song.link ?? ""
                };
                db.TbSongs.Add(songDb);
                db.SaveChanges();

            }
            //connect song to show
            var showDb = db.TbShows.Where(x => x.ShowDescription == show.description&&x.PresentedBy==show.user.name).FirstOrDefault();
            if (showDb == null)
            {
                Console.WriteLine("show not found");
                return;
            }
            var songShow = new TbSongsShow()
            {
                ShowId = showDb.Id,
                SongId = songDb.Id
            };
            db.TbSongsShows.Add(songShow);
            //db.SaveChanges();

            //save song playcount and chartplacing
            songDb.PlayCount = song.playcount;
            songDb.ChartPlacings = song.monthlyChartPlacings;
            db.SaveChanges();


        }

        private static void AddShow(DataShow data)
        {
            var showDB = db.TbShows.Where(x => x.ShowDescription == data.description && x.PresentedBy == data.user.name).FirstOrDefault();
            if (showDB == null)
            {
                showDB = new TbShow()
                {
                    ShowDescription = data.description,
                    PresentedBy = data.user.name
                };
                db.TbShows.Add(showDB);
                db.SaveChanges();
            }
        }

        private static void ClientEvent(object? sender, EventArgs e)
        {
            Console.WriteLine(e.ToString());
        }


        private static void OnResponseReceived(string eventName, SocketIOResponse response)
        {
            if (eventName == "song")
            {
                return;
            }
            if (eventName == "presenter")
            {
                Console.WriteLine("Recieved presenter info");
                return;
            }
            Console.WriteLine($"Event: {eventName}, Response: {response}");
        }

    }
}
