using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yogyakarta_Effective_Route.BingRoutingService;

namespace Yogyakarta_Effective_Route.Models
{
    public class Gene
    {
        public List<TouristObject> objects { get; set; }
        public double fitness { get; set; }

        public List<int> getIDs()
        {
            List<int> ids = new List<int>();
            for (int i = 0; i < objects.Count; i++)
            {
                ids.Add(objects[i].ID);
            }
            return ids;
        }

        public static Gene CalcFitness(Gene gene)
        {
            RouteRequest request = new RouteRequest();
            request.Credentials = new Microsoft.Maps.MapControl.WPF.Credentials { ApplicationId = "AgchoLh-mhsNBNEjRvUTN0kKhoNOsc_nnezXlasG6FJArss7xgt38h5cNp99N814" };
            Waypoint[] waypoints = new Waypoint[gene.objects.Count];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = new Waypoint() { Location = new Location() { Longitude = gene.objects[i].Longitude, Latitude = gene.objects[i].Lattitude } };
            }
            request.Waypoints = waypoints;
            request.UserProfile = new UserProfile()
            {
                DistanceUnit = DistanceUnit.Kilometer
            };
            request.Options = new RouteOptions()
            {
                Mode = TravelMode.Driving,
                Optimization = RouteOptimization.MinimizeDistance
            };
            RouteServiceClient client = new RouteServiceClient("BasicHttpBinding_IRouteService");
            RouteResponse response = client.CalculateRoute(request);
            RouteResult result = response.Result;
            gene.fitness = result.Summary.Distance;
            return gene;
        }
    }
}
