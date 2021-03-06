using System.Collections.Generic;
using System.Linq;

namespace Sandbox.Challenges.TravelingSalesperson;

/// <summary>
/// Represents a <see cref="NearestNeighborPathBuilder"/> that utilizes the nearest neighbor algorithm to generate an "optimal" path.
/// </summary>
/// <see href="https://en.wikipedia.org/wiki/Nearest_neighbour_algorithm"/>
internal sealed class NearestNeighborPathBuilder : PathBuilder {

    #region Fields

    private double _processedPaths;
    private double _totalPaths;

    #endregion

    #region Constructor

    /// <summary>
    /// Creates a new instance of <see cref="NearestNeighborPathBuilder"/> with the specified cities.
    /// </summary>
    /// <param name="cities">The cities this path builder will use to build paths.</param>
    public NearestNeighborPathBuilder(IEnumerable<City> cities) : base(cities) => _totalPaths = cities.Count();

    #endregion

    #region Public Methods

    public override IEnumerable<CityPath> GenerateAll() {
        List<CityPath> paths = new List<CityPath>();
        foreach (City city in Cities)
        {
            CityPath? path = GeneratePath(city);
            if (path != null)
                paths.Add(path);
        }

        return paths;
    }

    #endregion

    #region Private Methods

    private CityPath? GeneratePath(City startingCity) {
        IEnumerable<City> remainingCities = Cities.Where(city => city.Id != startingCity.Id);
        City? nextCity = GetNearestCity(startingCity, remainingCities.ToArray());
        if (nextCity != null)
        {
            List<City> cities = new List<City> { startingCity, nextCity };

            while (remainingCities.Count() > 0)
            {
                remainingCities = Cities.Where(w => !cities.Any(a => a.Id == w.Id));
                if (nextCity != null)
                {
                    nextCity = GetNearestCity(nextCity, remainingCities);
                    if (nextCity != null)
                        cities.Add(nextCity);
                }
            }

            OnProgressChanged(++_processedPaths, _totalPaths);
            return new CityPath(cities);
        }

        return null;
    }
    private static City? GetNearestCity(City currentCity, IEnumerable<City> remainingCities) {
        double shortestDistance = double.MaxValue;
        City? nearestCity = null;
        foreach (City city in remainingCities) {
            double distance = currentCity.DistanceTo(city);
            if (distance < shortestDistance) {
                shortestDistance = distance;
                nearestCity = city;
            }
        }

        return nearestCity;
    }

    #endregion

}