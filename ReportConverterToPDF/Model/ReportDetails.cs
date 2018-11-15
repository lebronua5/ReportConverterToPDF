using System.Xml.Serialization;
using System.Collections.Generic;
using ReportConverterToPDF.Interfaces;

namespace ReportConverterToPDF.Model
{
    [XmlRoot(ElementName = "Car_SpecEqipment")]
    public class Car_SpecEqipment
    {
        [XmlAttribute(AttributeName = "idSpeshEquipment")]
        public string IdSpeshEquipment { get; set; }
        [XmlAttribute(AttributeName = "idStatus")]
        public string IdStatus { get; set; }
        [XmlAttribute(AttributeName = "SpecEquipment")]
        public string SpecEquipment { get; set; }
        [XmlAttribute(AttributeName = "StateEq")]
        public string StateEq { get; set; }
    }

    [XmlRoot(ElementName = "Trailer_SpecEqipment")]
    public class Trailer_SpecEqipment 
    {
        [XmlAttribute(AttributeName = "idSpeshEquipment")]
        public string IdSpeshEquipment { get; set; }
        [XmlAttribute(AttributeName = "idStatus")]
        public string IdStatus { get; set; }
        [XmlAttribute(AttributeName = "SpecEquipment")]
        public string SpecEquipment { get; set; }
        [XmlAttribute(AttributeName = "StateEq")]
        public string StateEq { get; set; }
    }

    [XmlRoot(ElementName = "Car_SpecEqipments")]
    public class Car_SpecEqipments
    {
        [XmlElement(ElementName = "Car_SpecEqipment")]
        public List<Car_SpecEqipment> Car_SpecEqipment { get; set; }
    }

   

    [XmlRoot(ElementName = "Trailer_SpecEqipments")]
    public class Trailer_SpecEqipments
    {
        [XmlElement(ElementName = "Trailer_SpecEqipment")]
        public List<Trailer_SpecEqipment> Trailer_SpecEqipment { get; set; }
    }

    [XmlRoot(ElementName = "Technic")]
    public class Technic
    {
        [XmlAttribute(AttributeName = "idRefCompl")]
        public string IdRefCompl { get; set; }
        [XmlAttribute(AttributeName = "Checked")]
        public string Checked { get; set; }
        [XmlAttribute(AttributeName = "Comment")]
        public string Comment { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "ComplectTechnic")]
    public class ComplectTechnic
    {
        [XmlElement(ElementName = "Technic")]
        public List<Technic> Technic { get; set; }
    }

    [XmlRoot(ElementName = "Document")]
    public class Document
    {
        [XmlAttribute(AttributeName = "idRefDocs")]
        public string IdRefDocs { get; set; }
        [XmlAttribute(AttributeName = "checked")]
        public string Checked { get; set; }
        [XmlAttribute(AttributeName = "comment")]
        public string Comment { get; set; }
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "ComplectDocs")]
    public class ComplectDocs
    {
        [XmlElement(ElementName = "Document")]
        public List<Document> Document { get; set; }
    }

    [XmlRoot(ElementName = "CarFuel")]
    public class CarFuel
    {
        [XmlAttribute(AttributeName = "idTypeFuel")]
        public string IdTypeFuel { get; set; }
        [XmlAttribute(AttributeName = "FuelName")]
        public string FuelName { get; set; }
        [XmlAttribute(AttributeName = "RestFuel")]
        public string RestFuel { get; set; }
        [XmlAttribute(AttributeName = "RestFuelFact")]
        public string RestFuelFact { get; set; }
    }

    [XmlRoot(ElementName = "CarFuels")]
    public class CarFuels
    {
        [XmlElement(ElementName = "CarFuel")]
        public List<CarFuel> CarFuel { get; set; }
    }

    [XmlRoot(ElementName = "TrailerFuel")]
    public class TrailerFuel
    {
        [XmlAttribute(AttributeName = "idTypeFuel")]
        public string IdTypeFuel { get; set; }
        [XmlAttribute(AttributeName = "FuelName")]
        public string FuelName { get; set; }
        [XmlAttribute(AttributeName = "RestFuel")]
        public string RestFuel { get; set; }
        [XmlAttribute(AttributeName = "RestFuelFact")]
        public string RestFuelFact { get; set; }
    }

    [XmlRoot(ElementName = "TrailerFuels")]
    public class TrailerFuels
    {
        [XmlElement(ElementName = "TrailerFuel")]
        public TrailerFuel TrailerFuel { get; set; }
    }

    [XmlRoot(ElementName = "Tire")]
    public class Tire
    {
        [XmlAttribute(AttributeName = "idCarModification")]
        public string IdCarModification { get; set; }
        [XmlAttribute(AttributeName = "idModel")]
        public string IdModel { get; set; }
        [XmlAttribute(AttributeName = "idPlace")]
        public string IdPlace { get; set; }
        [XmlAttribute(AttributeName = "idTire")]
        public string IdTire { get; set; }
        [XmlAttribute(AttributeName = "idState")]
        public string IdState { get; set; }
        [XmlAttribute(AttributeName = "PlaceName")]
        public string PlaceName { get; set; }
        [XmlAttribute(AttributeName = "StateName")]
        public string StateName { get; set; }
        [XmlAttribute(AttributeName = "TypePict")]
        public string TypePict { get; set; }
        [XmlAttribute(AttributeName = "SerialNumber")]
        public string SerialNumber { get; set; }
        [XmlAttribute(AttributeName = "TotalKM")]
        public string TotalKM { get; set; }
        [XmlAttribute(AttributeName = "ModelName")]
        public string ModelName { get; set; }
        [XmlAttribute(AttributeName = "pressure")]
        public string Pressure { get; set; }
        [XmlAttribute(AttributeName = "protectorMM")]
        public string ProtectorMM { get; set; }       
    }

    [XmlRoot(ElementName = "CarTires")]
    public class CarTires
    {
        [XmlElement(ElementName = "Tire")]
        public List<Tire> Tire { get; set; }
    }

    [XmlRoot(ElementName = "TrailerTires")]
    public class TrailerTires
    {
        [XmlElement(ElementName = "Tire")]
        public List<Tire> Tire { get; set; }
    }

    [XmlRoot(ElementName = "Range")]
    public class Range
    {
        [XmlAttribute(AttributeName = "oilRange")]
        public string OilRange { get; set; }
        [XmlAttribute(AttributeName = "windowWasher")]
        public string WindowWasher { get; set; }
        [XmlAttribute(AttributeName = "idForeShoot")]
        public string IdForeShoot { get; set; }
        [XmlAttribute(AttributeName = "URL_photo")]
        public string URL_photo { get; set; }
        [XmlAttribute(AttributeName = "comment")]
        public string ForeShoot { get; set; }
        [XmlAttribute(AttributeName = "ForeShoot")]        
        public string Comment { get; set; }
        public byte[] ImageContent { get;set; }

        [XmlAttribute(AttributeName = "antifreeze")]
        public string Antifreeze { get; set; }
    }

    [XmlRoot(ElementName = "Ranges")]
    public class Ranges
    {
        [XmlElement(ElementName = "Range")]
        public Range Range { get; set; }
    }

    [XmlRoot(ElementName = "Photos")]
    public class Photos
    {
        [XmlElement(ElementName = "Range")]
        public List<Range> Range { get; set; }
    }

    [XmlRoot(ElementName = "Header")]
    public class ReportDetails
    {
        [XmlElement(ElementName = "Car_SpecEqipments")]
        public Car_SpecEqipments Car_SpecEqipments { get; set; }
        [XmlElement(ElementName = "Trailer_SpecEqipments")]
        public Trailer_SpecEqipments Trailer_SpecEqipments { get; set; }
        [XmlElement(ElementName = "ComplectTechnic")]
        public ComplectTechnic ComplectTechnic { get; set; }
        [XmlElement(ElementName = "ComplectDocs")]
        public ComplectDocs ComplectDocs { get; set; }
        [XmlElement(ElementName = "CarFuels")]
        public CarFuels CarFuels { get; set; }
        [XmlElement(ElementName = "TrailerFuels")]
        public TrailerFuels TrailerFuels { get; set; }
        [XmlElement(ElementName = "CarTires")]
        public CarTires CarTires { get; set; }
        [XmlElement(ElementName = "TrailerTires")]
        public TrailerTires TrailerTires { get; set; }
        [XmlElement(ElementName = "Ranges")]
        public Ranges Ranges { get; set; }
        [XmlElement(ElementName = "Photos")]
        public Photos Photos { get; set; }
        [XmlAttribute(AttributeName = "idVCMH")]
        public string IdVCMH { get; set; }
        [XmlAttribute(AttributeName = "idTransportCar")]
        public string IdTransportCar { get; set; }
        [XmlAttribute(AttributeName = "idTrailer")]
        public string IdTrailer { get; set; }
        [XmlAttribute(AttributeName = "idDriver")]
        public string IdDriver { get; set; }
        [XmlAttribute(AttributeName = "DateCreate")]
        public string DateCreate { get; set; }
        [XmlAttribute(AttributeName = "idDriver_Accept")]
        public string IdDriver_Accept { get; set; }
        [XmlAttribute(AttributeName = "idNK")]
        public string IdNK { get; set; }
        [XmlAttribute(AttributeName = "driverFIO")]
        public string DriverFIO { get; set; }
        [XmlAttribute(AttributeName = "Ak_name")]
        public string Ak_name { get; set; }
        [XmlAttribute(AttributeName = "NumberGosReg")]
        public string NumberGosReg { get; set; }
        [XmlAttribute(AttributeName = "MarkaName")]
        public string MarkaName { get; set; }
        [XmlAttribute(AttributeName = "ModelName")]
        public string ModelName { get; set; }
        [XmlAttribute(AttributeName = "trailerNumGosReg")]
        public string TrailerNumGosReg { get; set; }
        [XmlAttribute(AttributeName = "driverAcceptFIO")]
        public string DriverAcceptFIO { get; set; }
        [XmlAttribute(AttributeName = "NK_FIO")]
        public string NK_FIO { get; set; }
        [XmlAttribute(AttributeName = "UrFace")]
        public string UrFace { get; set; }
        [XmlAttribute(AttributeName = "Boss")]
        public string Boss { get; set; }
    }

}
