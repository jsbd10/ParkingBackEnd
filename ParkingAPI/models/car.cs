using MongoDB.Bson.Serialization.Attributes;

namespace ParkingAPI.models
{
    public class car
    { 
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string id { get; set; }
        
       [BsonElement("placa")]
        public string placa { get; set; }
        
        [BsonElement("marca")]
        public string marca { get; set; }
        
        [BsonElement("color")]
        public string color { get; set; }
        
        [BsonElement("tipo")]
        public string tipo { get; set; }
        
        [BsonElement("fechaIngreso")]
        public string fechaIngreso { get; set; }
        
        [BsonElement("fechaSalida")]
        public string fechaSalida { get; set; }
        
        [BsonElement("estado")]
        public string estado { get; set; }
        
        [BsonElement("precio")]
        public string precio { get; set; }
    }
}