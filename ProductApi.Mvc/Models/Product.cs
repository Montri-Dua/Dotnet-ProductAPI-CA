namespace ProductApi.Mvc.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string OPC { get; set; }
        public string CLO { get; set; }
        
        public string ItemCode { get; set; }
        public string RPL { get; set; }

        public DateTime ModifyDateTime { get; set; }
    }
}