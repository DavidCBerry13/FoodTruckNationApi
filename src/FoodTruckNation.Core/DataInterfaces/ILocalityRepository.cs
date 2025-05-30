using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodTruckNation.Core.Domain;

namespace FoodTruckNation.Core.DataInterfaces
{
    public interface ILocalityRepository
    {

        public Locality GetLocality(string localityCode);


        public List<Locality> GetLocalities();


        public void Save(Locality locality);


        public void Delete(Locality locality);

    }
}
