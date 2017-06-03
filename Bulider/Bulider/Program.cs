using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulider
{
    //мука
    class Flour
    {
        // какого сорта мука
        public string Sort { get; set; }
    }
    // соль
    class Salt
    { }
    // пищевые добавки
    class Additives
    {
        public string Name { get; set; }
    }

    class Bread
    {
        // пшеничная мука
        public Flour WheatFlour { get; set; }
        // ржаная мука
        public Flour RyeFlour { get; set; }
        // соль
        public Salt Salt { get; set; }
        // пищевые добавки
        public Additives Additives { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (WheatFlour != null)
                sb.Append("Пшеничная мука " + WheatFlour.Sort + "\n");
            if (RyeFlour != null)
                sb.Append("Ржаная мука " + RyeFlour.Sort + " \n");
            if (Salt != null)
                sb.Append("Соль \n");
            if (Additives != null)
                sb.Append("Добавки: " + Additives.Name + " \n");
            return sb.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // содаем объект пекаря
            Baker baker = new Baker();
            // создаем билдер для ржаного хлеба
            BreadBuilder builder = new RyeBreadBuilder();
            // выпекаем
            Bread ryeBread = baker.Bake(builder);
            Console.WriteLine(ryeBread.ToString());
            // оздаем билдер для пшеничного хлеба
            builder = new WheatBreadBuilder();
            Bread wheatBread = baker.Bake(builder);
            Console.WriteLine(wheatBread.ToString());

            Console.Read();
        }
    }
    // абстрактный класс строителя
    abstract class BreadBuilder
    {
        public Bread Bread { get; private set; }
        public void CreateBread()
        {
            Bread = new Bread();
        }
        public abstract void SetWheatFlour();
        public abstract void SetRyeFlour();
        public abstract void SetSalt();
        public abstract void SetAdditives();
    }
    // пекарь
    class Baker
    {
        public Bread Bake(BreadBuilder breadBuilder)
        {
            breadBuilder.CreateBread();
            breadBuilder.SetWheatFlour();
            breadBuilder.SetRyeFlour();
            breadBuilder.SetSalt();
            breadBuilder.SetAdditives();
            return breadBuilder.Bread;
        }
    }
    // строитель для ржаного хлеба
    class RyeBreadBuilder : BreadBuilder
    {
        public override void SetWheatFlour()
        {
            // не используется
        }

        public override void SetRyeFlour()
        {
            this.Bread.RyeFlour = new Flour { Sort = "1 сорт" };
        }

        public override void SetSalt()
        {
            this.Bread.Salt = new Salt();
        }

        public override void SetAdditives()
        {
            // не используется
        }
    }
    // строитель для пшеничного хлеба
    class WheatBreadBuilder : BreadBuilder
    {
        public override void SetWheatFlour()
        {
            this.Bread.WheatFlour = new Flour { Sort = "высший сорт" };
        }

        public override void SetRyeFlour()
        {
            // не используется
        }

        public override void SetSalt()
        {
            this.Bread.Salt = new Salt();
        }

        public override void SetAdditives()
        {
            this.Bread.Additives = new Additives { Name = "улучшитель хлебопекарный" };
        }
    }
}
