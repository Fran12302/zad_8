namespace dz_8
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
          Equipment equipment= new Equipment("volan");
            CarConfiguration configuration= new CarConfiguration("audi");
            configuration.AddEqupement(equipment);
            ConfigurationManger manager = new ConfigurationManger(configuration);
            manager.SaveToManager();
            configuration.AddEqupement(new Equipment("kocnica"));
            manager.SaveToManager();
            foreach(var equip in configuration.additionalEquipment)
            {
                Console.WriteLine(equip.Name);
            }
            configuration.AddEqupement(new Equipment("brisac"));
            manager.Undo();
            foreach (var equip in configuration.additionalEquipment)
            {
                Console.WriteLine(equip.Name);
            }
        }
    }
    public class Equipment
    {
        public Equipment(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
    public class CarConfigurator//memento
    {
        private string model;
        private List<Equipment> additionalEquipment = new List<Equipment>();
        
        public CarConfigurator(string model, List<Equipment>additionalEquipment)
        {
            model=model;
            this.additionalEquipment = additionalEquipment;
        }
        public string GetModel() {
            return this.model; }

        public List<Equipment> GetEquipement()
        {
            return this.additionalEquipment;
        }

    }

    public class CarConfiguration//origniator
    {
        private string model;
        public List<Equipment> additionalEquipment;

        public CarConfiguration(string model)
        {
            this.model = model;
            additionalEquipment=new List<Equipment>();
        }

        public void AddEqupement(Equipment equipment)
        {
            additionalEquipment.Add(equipment);
        }
        public void RemoveEqupement(Equipment equipment)
        {
            additionalEquipment.Remove(equipment);
        }
        public CarConfigurator Save()
        {
            return new CarConfigurator(model, additionalEquipment);
        }

        public void Restore(CarConfigurator configurator)
        {
           model=configurator.GetModel();
            additionalEquipment= configurator.GetEquipement();
        }
    }

    public class ConfigurationManger//skrbnik
    {
        CarConfiguration carConfiguration;
        private List<CarConfigurator> configurators = new List<CarConfigurator>();

        public ConfigurationManger(CarConfiguration carConfiguration)
        {
            this.carConfiguration = carConfiguration;
        }
        public void SaveToManager() { configurators.Add(carConfiguration.Save()); }
       
        public void Undo()
        {
            int count = configurators.Count;
            if (count > 1)
            {
                carConfiguration.Restore(configurators.ElementAt(count - 2));
            }
        }
    }
}