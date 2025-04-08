using System;
using System.Collections.Generic;

namespace ChimiMiBurrica
{
    // Clase para representar los ingredientes
    public class Ingrediente
    {
        public string Nombre { get; private set; }
        public decimal Precio { get; private set; }

        public Ingrediente(string nombre, decimal precio)
        {
            Nombre = nombre;
            Precio = precio;
        }
    }

    // Clase base para todas las hamburguesas
    public abstract class Hamburguesa
    {
        // Propiedades comunes
        public string TipoPan { get; protected set; }
        public string TipoCarne { get; protected set; }
        public decimal PrecioBase { get; protected set; }
        protected List<Ingrediente> IngredientesAdicionales;

        // Constructor base
        public Hamburguesa(string tipoPan, string tipoCarne, decimal precioBase)
        {
            TipoPan = tipoPan;
            TipoCarne = tipoCarne;
            PrecioBase = precioBase;
            IngredientesAdicionales = new List<Ingrediente>();
        }

        // Método para agregar ingredientes (si lo permite la clase)
        public virtual bool AgregarIngrediente(Ingrediente ingrediente)
        {
            IngredientesAdicionales.Add(ingrediente);
            return true;
        }

        // Método para calcular el precio total
        public decimal CalcularPrecioTotal()
        {
            decimal precioTotal = PrecioBase;
            
            foreach (var ingrediente in IngredientesAdicionales)
            {
                precioTotal += ingrediente.Precio;
            }
            
            return precioTotal;
        }

        // Método para mostrar los detalles completos (ingredientes y precios)
        public string MostrarDetalles()
        {
            string detalles = $"Hamburguesa con pan {TipoPan}, carne {TipoCarne}\n";
            detalles += $"Precio base: ${PrecioBase:F2}\n";
            
            if (IngredientesAdicionales.Count > 0)
            {
                detalles += "Ingredientes adicionales:\n";
                decimal precioAdicionales = 0;
                
                foreach (var ingrediente in IngredientesAdicionales)
                {
                    detalles += $"- {ingrediente.Nombre}: ${ingrediente.Precio:F2}\n";
                    precioAdicionales += ingrediente.Precio;
                }
                
                detalles += $"Subtotal adicionales: ${precioAdicionales:F2}\n";
            }
            
            detalles += $"Precio total: ${CalcularPrecioTotal():F2}";
            
            return detalles;
        }
    }

    // Clase para la hamburguesa clásica
    public class HamburguesaClasica : Hamburguesa
    {
        private const int MaximoIngredientes = 4;

        public HamburguesaClasica(string tipoPan, string tipoCarne, decimal precioBase) 
            : base(tipoPan, tipoCarne, precioBase)
        {
        }

        public override bool AgregarIngrediente(Ingrediente ingrediente)
        {
            // Verificar si ya alcanzó el máximo de ingredientes
            if (IngredientesAdicionales.Count >= MaximoIngredientes)
            {
                Console.WriteLine("No se pueden agregar más ingredientes. Máximo alcanzado.");
                return false;
            }
            
            return base.AgregarIngrediente(ingrediente);
        }
    }

    // Clase para la hamburguesa saludable
    public class HamburguesaSaludable : Hamburguesa
    {
        private const int MaximoIngredientes = 6;

        public HamburguesaSaludable(string tipoCarne, decimal precioBase) 
            : base("Integral", tipoCarne, precioBase)
        {
        }

        public override bool AgregarIngrediente(Ingrediente ingrediente)
        {
            // Verificar si ya alcanzó el máximo de ingredientes
            if (IngredientesAdicionales.Count >= MaximoIngredientes)
            {
                Console.WriteLine("No se pueden agregar más ingredientes. Máximo alcanzado.");
                return false;
            }
            
            return base.AgregarIngrediente(ingrediente);
        }
    }

    // Clase para la hamburguesa premium
    public class HamburguesaPremium : Hamburguesa
    {
        public HamburguesaPremium(string tipoPan, string tipoCarne, decimal precioBase) 
            : base(tipoPan, tipoCarne, precioBase)
        {
            // Agregar papitas y bebida automáticamente
            base.AgregarIngrediente(new Ingrediente("Papitas", 3.50m));
            base.AgregarIngrediente(new Ingrediente("Bebida", 2.50m));
        }

        // No se permiten ingredientes adicionales
        public override bool AgregarIngrediente(Ingrediente ingrediente)
        {
            Console.WriteLine("Las hamburguesas premium no permiten ingredientes adicionales.");
            return false;
        }
    }

    // Clase principal del programa
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido a Chimi MiBurrica");
            Console.WriteLine("============================\n");

            // Crear una hamburguesa clásica
            var clasica = new HamburguesaClasica("Normal", "Res", 10.00m);
            clasica.AgregarIngrediente(new Ingrediente("Lechuga", 0.50m));
            clasica.AgregarIngrediente(new Ingrediente("Tomate", 0.75m));
            clasica.AgregarIngrediente(new Ingrediente("Queso", 1.50m));
            clasica.AgregarIngrediente(new Ingrediente("Bacon", 2.00m));
            
            // Crear una hamburguesa saludable
            var saludable = new HamburguesaSaludable("Pollo", 12.00m);
            saludable.AgregarIngrediente(new Ingrediente("Aguacate", 2.00m));
            saludable.AgregarIngrediente(new Ingrediente("Espinaca", 1.00m));
            
            // Crear una hamburguesa premium
            var premium = new HamburguesaPremium("Brioche", "Res Angus", 18.00m);
            
            // Mostrar los detalles de cada hamburguesa
            Console.WriteLine("HAMBURGUESA CLÁSICA:");
            Console.WriteLine(clasica.MostrarDetalles());
            Console.WriteLine("\nHAMBURGUESA SALUDABLE:");
            Console.WriteLine(saludable.MostrarDetalles());
            Console.WriteLine("\nHAMBURGUESA PREMIUM:");
            Console.WriteLine(premium.MostrarDetalles());
            
            Console.ReadKey();
        }
    }
}
