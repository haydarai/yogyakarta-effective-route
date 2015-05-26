using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Yogyakarta_Effective_Route.BingRoutingService;
using Yogyakarta_Effective_Route.Helpers;
using Yogyakarta_Effective_Route.Models;

namespace Yogyakarta_Effective_Route
{
    public partial class MainWindow : Window
    {
        private List<TouristObject> touristobjects;
        private List<TouristObject> selectedtouristobjects;
        private GeoCoordinateWatcher gcw;
        private Location currentlocation;
        private Gene bestGene;

        public MainWindow()
        {
            InitializeComponent();
            gcw = new GeoCoordinateWatcher();
            gcw.StatusChanged += gcw_StatusChanged;
            gcw.Start();
            touristobjects = new List<TouristObject>() {
                new TouristObject() { ID = 1, Name = "Ratu Boko", Description = "Istana yang konon dipunyai oleh Keluarga Roro Jonggrang", Lattitude = -7.770542, Longitude = 110.489416},
                new TouristObject() { ID = 2, Name = "Parangtritis", Description = "Pantai yang terkenal dengan Legenda Nyi Roro Kidul", Lattitude = -8.0100571, Longitude = 110.3130087},
                new TouristObject() { ID = 3, Name = "Malioboro", Description = "Pusat perbelanjaan utama di tengah kota", Lattitude = -7.792862, Longitude = 110.366006},
                new TouristObject() { ID = 4, Name = "Kaliurang", Description = "Bagian lereng Merapi yang terkenal", Lattitude = -7.6000, Longitude = 110.4167},
                new TouristObject() { ID = 5, Name = "Kali Code", Description = "Salah satu sungai yang terkenal", Lattitude = -7.7991479, Longitude = 110.3716296},
                new TouristObject() { ID = 6, Name = "Candi Prambanan", Description = "Candi yang terkenal dengan Legenda Roro Jonggrang", Lattitude = -7.751919, Longitude = 110.492006},
                new TouristObject() { ID = 7, Name = "Museum Sonobudoyo", Description = "Museum yang berada di area Kraton", Lattitude = -7.802431, Longitude = 110.363953},
                new TouristObject() { ID = 8, Name = "Benteng Vredeburg", Description = "Peninggalan Benteng Belanda yang digunakan untuk mengawasi Kraton", Lattitude = -7.800289, Longitude = 110.366028},
                new TouristObject() { ID = 9, Name = "Keraton Yogyakarta", Description = "Tempat tinggal Raja dan Keluarganya", Lattitude = -7.806553, Longitude = 110.364057},
            };
            selectedtouristobjects = new List<TouristObject>();
            lvTouristObjects.ItemsSource = touristobjects;
        }

        private void lvTouristObjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (TouristObject item in e.AddedItems)
            {
                selectedtouristobjects.Add(item);
            }

            foreach (TouristObject item in e.RemovedItems)
            {
                selectedtouristobjects.Remove(item);
            }
        }

        private void bCalculate_Click(object sender, RoutedEventArgs e)
        {
            ResultPanel.Children.Clear();
            mRoute.Children.Clear();
            if (currentlocation == null)
            {
                MessageBox.Show("Please wait a little longer while our program fetching your current location");
                return;
            }
            Population population = initPopulation(selectedtouristobjects, 10);
            // Calculate Fitness
            for (int i = 0; i < population.genes.Count; i++)
            {
                Gene.CalcFitness(population.genes[i]);
                population.genes[i] = Gene.CalcFitness(population.genes[i]);
            }

            for (int iteration = 0; iteration < 10; iteration++)
            {
                // Selection
                Population oldpopulation = population;
                List<Population> selectedandnotselected = Selection.RWSSelection(population);
                population = selectedandnotselected[0];
                Population notselectedpopulation = selectedandnotselected[1];
                if (population.genes.Count < 2)
                {
                    population = oldpopulation;
                    notselectedpopulation.genes = null;
                }
                if (population.genes.Count % 2 != 0)
                {
                    Population move = new Population();
                    move.genes = new List<Gene>();
                    move.genes.Add(population.genes.ElementAt(population.genes.Count - 1));
                    notselectedpopulation = move;
                    population.genes.RemoveAt(population.genes.Count - 1);
                }

                // Crossover
                List<List<int>> newpop = new List<List<int>>();
                for (int i = 0; i < population.genes.Count; i++)
                {
                    newpop.Add(population.genes[i].getIDs());
                }
                Crossover crossover = new Crossover();
                List<List<int>> newnewpop = new List<List<int>>();
                for (int i = 0; i < newpop.Count; i = i + 2)
                {
                    crossover.Order(newpop[i], newpop[i + 1]);
                    newnewpop.Add(crossover.getOffspring());
                    newnewpop.Add(crossover.getOffspring2());
                }
                // Mutation
                for (int i = 0; i < newnewpop.Count; i++)
                {
                    newnewpop[i] = Mutation.SwapMutation(newnewpop[i]);
                }

                // Update Generation
                Population newpopulation = new Population();
                newpopulation.genes = new List<Gene>();
                for (int i = 0; i < newnewpop.Count; i++)
                {
                    List<TouristObject> objeeects = new List<TouristObject>();
                    objeeects.Add(new TouristObject() { ID = 0, Name = "Current Location", Description = "Current Location", Lattitude = currentlocation.Latitude, Longitude = currentlocation.Longitude });
                    for (int j = 1; j < newnewpop[i].Count; j++)
                    {
                        TouristObject objeect = new TouristObject();
                        objeect = touristobjects[newnewpop[i][j] - 1];
                        objeeects.Add(objeect);
                    }
                    Gene gene = new Gene() { objects = objeeects };
                    newpopulation.genes.Add(gene);
                }
                // Calculate Fitness
                for (int i = 0; i < newpopulation.genes.Count; i++)
                {
                    Gene.CalcFitness(newpopulation.genes[i]);
                    newpopulation.genes[i] = Gene.CalcFitness(newpopulation.genes[i]);
                }
                population = newpopulation;
                if (notselectedpopulation.genes != null)
                {
                    population.genes = population.genes.Concat(notselectedpopulation.genes).ToList();
                }
            }

            int indexBest = 0;
            double valueBest = double.MaxValue;
            for (int i = 0; i < population.genes.Count; i++)
            {
                if (population.genes[i].fitness < valueBest)
                {
                    valueBest = population.genes[i].fitness;
                    indexBest = i;
                }
            }
            bestGene = population.genes[indexBest];
            // Draw
            Microsoft.Maps.MapControl.WPF.MapPolyline polyline = new Microsoft.Maps.MapControl.WPF.MapPolyline();
            polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue);
            polyline.StrokeThickness = 1;
            polyline.Opacity = 1;
            Microsoft.Maps.MapControl.WPF.LocationCollection locationcollection = new Microsoft.Maps.MapControl.WPF.LocationCollection();
            Microsoft.Maps.MapControl.WPF.Pushpin pushpin;
            for (int i = 0; i < bestGene.objects.Count; i++)
            {
                pushpin = new Microsoft.Maps.MapControl.WPF.Pushpin() { Location = new Microsoft.Maps.MapControl.WPF.Location() { Latitude = bestGene.objects[i].Lattitude, Longitude = bestGene.objects[i].Longitude } };
                locationcollection.Add(new Microsoft.Maps.MapControl.WPF.Location(bestGene.objects[i].Lattitude, bestGene.objects[i].Longitude));
                pushpin.Content = bestGene.objects[i].Name;
                mRoute.Children.Add(pushpin);
                TextBlock tb = new TextBlock();
                if (i > 0)
                {
                    tb.Text = i.ToString() + " " + bestGene.objects[i].Name;
                    ResultPanel.Children.Add(tb);
                }
            }
            TextBlock tbDistance = new TextBlock();
            tbDistance.Text = "Total Distance: " + bestGene.fitness.ToString() + " km ";
            ResultPanel.Children.Add(tbDistance);
            polyline.Locations = locationcollection;
            mRoute.Children.Add(polyline);
        }

        private Population initPopulation(List<TouristObject> touristobjects, int populationSize)
        {
            List<int> integers = new List<int>();
            for (int i = 0; i < touristobjects.Count; i++)
            {
                integers.Add(touristobjects[i].ID);
            }
            Random r = new Random();
            Population population = new Population();
            population.genes = new List<Gene>();
            for (int j = 0; j < populationSize; j++)
            {
                List<int> ints = (from k in integers orderby r.Next(integers.Count) select k).ToList();
                Gene gene = new Gene();
                gene.objects = new List<TouristObject>();
                gene.objects.Add(new TouristObject() { ID = 0, Name = "Current Location", Description = "Current Location", Lattitude = currentlocation.Latitude, Longitude = currentlocation.Longitude });
                for (int k = 0; k < ints.Count; k++)
                {
                    gene.objects.Add(touristobjects[ints[k] - 1]);
                }
                population.genes.Add(gene);
            }
            return population;
        }

        private void gcw_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            if (e.Status == GeoPositionStatus.Ready)
            {
                Microsoft.Maps.MapControl.WPF.Pushpin pushpin;
                if (gcw.Position.Location.IsUnknown)
                {
                    currentlocation = new Location() { Latitude = 110.3671027, Longitude = -7.7987857 };
                    pushpin = new Microsoft.Maps.MapControl.WPF.Pushpin() { Location = new Microsoft.Maps.MapControl.WPF.Location() { Latitude = currentlocation.Latitude, Longitude = currentlocation.Longitude } };
                }
                else
                {
                    currentlocation = new Location() { Latitude = gcw.Position.Location.Latitude, Longitude = gcw.Position.Location.Longitude };
                    pushpin = new Microsoft.Maps.MapControl.WPF.Pushpin() { Location = new Microsoft.Maps.MapControl.WPF.Location() { Latitude = currentlocation.Latitude, Longitude = currentlocation.Longitude } };
                }
                mRoute.Children.Add(pushpin);
            }
        }
    }
}
