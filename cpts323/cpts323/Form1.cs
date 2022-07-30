using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
namespace cpts323
{
    public partial class Form1 : Form
    {

        FirebaseClient client = new FirebaseClient("https://heymotocarro-1a1d4.firebaseio.com/");
        Graphics G;
        Rectangle[] rect = new Rectangle[12];
        Beacons beacons;
        ParkingLot parkingLot;
        Sensors sensors;
        int[] testArray;

        
        public Form1()
        {
            InitializeComponent();
        }

        private void ParkingLot_Load(object sender, EventArgs e)
        {
            //G = this.CreateGraphics();
            //parkingLot = new ParkingLot(12,G);
            //intantaite the slot 1 - 12
            sensors = new Sensors(4);
            sensors.data[0].setCoordinates(0, 5);
            sensors.data[1].setCoordinates(9, 5);
            sensors.data[2].setCoordinates(0, 0);
            sensors.data[3].setCoordinates(9, 0);

     

            // Initialization for sensors can be a module like sensors setup
           
        }
        private void loadBtn_Click_1(object sender, EventArgs e)
        {
            G = this.CreateGraphics();
            parkingLot = new ParkingLot(12, G);
            getPopulationAsync();
        }
        private void button1_Click(object sender, EventArgs e)
        {



        }
        


        private async void getPopulationAsync() // grabs population from database 
        {


            //******************** Get initial list of beacons ***********************//
            beacons = await client
               .Child("Beacons/")//Prospect list
               .OnceSingleAsync<Beacons>();

           // getBeacons(BeaconsSet);

            //******************** Get changes on beacons ***********************//
            onChildChanged();


        }



        private static async Task sendData(int key)
        {
            FormUrlEncodedContent content;
            HttpResponseMessage response;
            HttpClient httpclient = new HttpClient();
            string responseString;
            int companyId = 3;

            var dictionary = new Dictionary<string, string>{
                            { "key",key.ToString()},
                            { "companyId",companyId.ToString()}
                        };

            content = new FormUrlEncodedContent(dictionary);
            response = await httpclient.PostAsync("https://us-central1-heymotocarro-1a1d4.cloudfunctions.net/sendData", content);
            responseString = await response.Content.ReadAsStringAsync();
            Response data = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(responseString);
            //Response message
            Console.WriteLine(data.key);
            Console.WriteLine(data.companyId);
        }

        private void onChildChanged() // Waits for data base to start with variable
        {


            var child = client.Child("Beacons/data");
            var observable = child.AsObservable<Beacon>();
            var subscription = observable
                .Subscribe(x =>
                {
                    int Key = Int32.Parse(x.Key);
                    beacons.data[Int32.Parse(x.Key)].update(x.Object);
                    Point p = beacons.data[Int32.Parse(x.Key)].getXY(sensors);
                   
                    if (p.y >= 0 && p.y <= 2)
                    {
                        int index = (int)Math.Floor(p.x / 1.5) + 6;
                        parkingLot.parkingSlots[index].ColorRed(G);

                    }
                    if (p.y >= 3 && p.y <= 5)
                    {
                        int index = (int)Math.Floor(p.x / 1.5);
                        parkingLot.parkingSlots[index].ColorRed(G);
                        
                    }
                   
                    


                });

        }

 

    }
}
