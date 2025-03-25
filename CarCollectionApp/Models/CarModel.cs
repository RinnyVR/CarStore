namespace CarCollectionApp.Models
{
    public class CarModel
    {
        public string Name { get; set; }
        public string Engine { get; set; }
        public string HP { get; set; }
        public string Torque { get; set; }
        public string Speed { get; set; }
        public string Image { get; set; }

        public CarModel(string name, string engine, string hp, string torque, string speed, string image)
        {
            Name = name;
            Engine = engine;
            HP = hp;
            Torque = torque;
            Speed = speed;
            Image = image;
        }
    }
}
