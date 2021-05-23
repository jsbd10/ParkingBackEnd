using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using ParkingAPI.models;

namespace ParkingAPI.Services
{
    public class CarServices
    {
        private IMongoCollection<car> _car;

        public CarServices(IcarSetting setting)
        {
            var cliente = new MongoClient(setting.Server);
            var database = cliente.GetDatabase(setting.Database);
            _car = database.GetCollection<car>(setting.Collection);
        }

        public List<car> Get()
        {
            return _car.Find(d=> d.estado.Equals("Activo")).ToList();
        }

        public car Create(car cars)
        {
            string nowEnter = string.Empty;
            DateTime current = DateTime.Now;
            nowEnter = current.ToString();
            car newCar = new car()
            {
                placa= cars.placa,
                marca= cars.marca,
                color= cars.color,
                tipo=cars.tipo,
                fechaIngreso= nowEnter,
                estado="Activo",
            };
            _car.InsertOne(newCar);
            return newCar;
        }

        public void Update(string id, car cars)
        {
            _car.ReplaceOne(cars => cars.id == id, cars);
        }

        public car getPrice(string placa)
        {
            car newCar = new car();
            string nowExit = string.Empty;
            DateTime currentExit = DateTime.Now;
            nowExit = currentExit.ToString();
            try
            {
            string a;
            List<car>carros= _car.Find(d => d.placa.Equals(placa)).ToList();
                 
            int cantCar = carros.Count();
                 newCar = carros[0];
                 if (cantCar==1)
                 {
                    if (string.IsNullOrEmpty(newCar.fechaSalida))
                     {
                         var filter = Builders<car>.Filter.Where(x => x.placa == newCar.placa);
                         var update = Builders<car>.Update.Set("fechaSalida", nowExit);
                         _car.UpdateOne(filter,update);
                     }
                 }

                 string priceFinal = CalcularPrecio(newCar);
                 if (cantCar>0)
                 {
                     if (string.IsNullOrEmpty(newCar.precio))
                     {
                         var filter = Builders<car>.Filter.Where(x => x.precio == newCar.precio);
                         var update = Builders<car>.Update.Set("precio", priceFinal);
                         _car.UpdateOne(filter,update);
                     }
                 }
                 if (cantCar>0)
                 {
                     if (newCar.estado.Equals("Activo"))
                     {
                         var filter = Builders<car>.Filter.Where(x => x.estado == newCar.estado);
                         var update = Builders<car>.Update.Set("estado", "Inactivo");
                         _car.UpdateOne(filter,update);
                     }
                 }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return newCar;
        }
        public string CalcularPrecio(car newCar)
        {
            DateTime fechaEntrada = Convert.ToDateTime(newCar.fechaIngreso);
            DateTime fechaSalida = Convert.ToDateTime(newCar.fechaSalida);
            
            DateTime date1 = new DateTime(fechaEntrada.Year, fechaEntrada.Month, 
                fechaEntrada.Day, fechaEntrada.Hour, fechaEntrada.Minute, fechaEntrada.Second);
            
            DateTime date2 = new DateTime(fechaSalida.Year, fechaSalida.Month, 
                fechaSalida.Day, fechaSalida.Hour, fechaSalida.Minute, fechaSalida.Second);

            TimeSpan ts = date2 - date1;

            double totalM = (ts.TotalSeconds)/60;
            double TotalPrice = 10 * totalM;
            Console.WriteLine(totalM);
            Console.WriteLine(TotalPrice);
            return TotalPrice.ToString();
        }
    }
}