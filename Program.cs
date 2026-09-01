using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto5_MantenimientoMaquinaria
{
    class Program
    {
        // ========================================
        // BLOQUE 1: CONFIGURACIÓN GLOBAL
        // ========================================
        const string ARCHIVO_MAQUINAS = "maquinas.txt";
        const string ARCHIVO_MANTENIMIENTOS = "mantenimientos.txt";
        const int MAX_REGISTROS = 100;

        // Arreglos paralelos para maquinaria
        static string[] codigosMaquina = new string[MAX_REGISTROS];
        static string[] nombresMaquina = new string[MAX_REGISTROS];
        static double[] horasUso = new double[MAX_REGISTROS];
        static double[] frecuenciaMantenimiento = new double[MAX_REGISTROS];
        static int totalMaquinas = 0;

        // Arreglos paralelos para mantenimientos
        static string[] codigoMantenimiento = new string[MAX_REGISTROS];
        static string[] fechaMantenimiento = new string[MAX_REGISTROS];
        static string[] tipoMantenimiento = new string[MAX_REGISTROS];
        static double[] costoMantenimiento = new double[MAX_REGISTROS];
        static int totalMantenimientos = 0;

        // ========================================
        // BLOQUE 2: MENÚ PRINCIPAL
        // ========================================
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            CargarMaquinas();
            CargarMantenimientos();

            int opcion;

            do
            {
                Console.Clear();
                MostrarEncabezado();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nMENÚ PRINCIPAL:");
                Console.ResetColor();

                Console.WriteLine("1. Registrar maquinaria");
                Console.WriteLine("2. Registrar mantenimiento");
                Console.WriteLine("3. Listar maquinaria y estado");
                Console.WriteLine("4. Máquinas que requieren mantenimiento");
                Console.WriteLine("5. Costo total de mantenimiento por máquina");
                Console.WriteLine("6. Generar reporte TXT");
                Console.WriteLine("7. Salir");

                Console.WriteLine("----------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Seleccione una opción: ");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = 0;
                }

                switch (opcion)
                {
                    case 1:
                        RegistrarMaquinaria();
                        break;

                    case 2:
                        RegistrarMantenimiento();
                        break;

                    case 3:
                        ListarMaquinaria();
                        break;

                    case 4:
                        MaquinasQueRequierenMantenimiento();
                        break;

                    case 5:
                        CostoTotalPorMaquina();
                        break;

                    case 6:
                        GenerarReporteTXT();
                        break;

                    case 7:
                        Console.WriteLine("Saliendo del sistema...");
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR: Opción no válida.");
                        Console.ResetColor();
                        break;
                } // Cierra switch

                if (opcion != 7)
                {
                    Console.WriteLine("\nPresione cualquier tecla para volver al menú...");
                    Console.ReadKey();
                }

            } while (opcion != 7); // Cierra el do-while

            // ========================================
            // DESDE AQUÍ LAS FUNCIONES ESTÁN FUERA DE MAIN
            // ========================================
        }

        // ========================================
        // BLOQUE 3: LÓGICA DE NEGOCIO
        // ========================================

        static void RegistrarMaquinaria()
        {
            if (totalMaquinas >= MAX_REGISTROS)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No se pueden registrar más máquinas.");
                return;
               

            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n--- REGISTRAR MAQUINARIA ---");
            Console.ResetColor();

            string codigo;

            do
            {
                Console.Write("Ingrese código de la máquina: ");
                codigo = Console.ReadLine().ToUpper();

                if (BuscarMaquinaPorCodigo(codigo) != -1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: El código ya existe.");
                    Console.ResetColor();
                }

            } while (BuscarMaquinaPorCodigo(codigo) != -1);

            Console.Write("Ingrese nombre de la máquina: ");
            string nombre = Console.ReadLine();

            double horas;
            do
            {
                Console.Write("Ingrese horas de uso acumuladas: ");

                if (!double.TryParse(Console.ReadLine(), out horas) || horas < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: Ingrese un número válido.");
                    horas = -1;
                    Console.ResetColor();
                }

            } while (horas < 0);

            double frecuencia;
            do
            {
                Console.Write("Ingrese frecuencia de mantenimiento (horas): ");

                if (!double.TryParse(Console.ReadLine(), out frecuencia) || frecuencia <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ERROR: La frecuencia debe ser mayor a 0.");
                    frecuencia = 0;
                    Console.ResetColor();
                }

            } while (frecuencia <= 0);

            codigosMaquina[totalMaquinas] = codigo;
            nombresMaquina[totalMaquinas] = nombre;
            horasUso[totalMaquinas] = horas;
            frecuenciaMantenimiento[totalMaquinas] = frecuencia;

            totalMaquinas++;

            GuardarMaquinas();

            Console.WriteLine("\n¡Maquinaria registrada correctamente!");
        }

        static int BuscarMaquinaPorCodigo(string codigo)
        {
            for (int i = 0; i < totalMaquinas; i++)
            {
                if (codigosMaquina[i] == codigo)
                {
                    return i;
                }
            }

            return -1;
        }

        static void RegistrarMantenimiento()
        {
            if (totalMantenimientos >= MAX_REGISTROS)
            {
                Console.WriteLine("No se pueden registrar más mantenimientos.");
                return;
            }

            Console.WriteLine("\n--- REGISTRAR MANTENIMIENTO ---");

            Console.Write("Ingrese código de la máquina: ");
            string codigo = Console.ReadLine().ToUpper();

            int indice = BuscarMaquinaPorCodigo(codigo);

            if (indice == -1)
            {
                Console.WriteLine("ERROR: La máquina no está registrada.");
                return;
            }

            Console.Write("Ingrese fecha (dd/MM/yyyy): ");
            string fecha = Console.ReadLine();

            int opcionTipo;

            do
            {
                Console.WriteLine("\nTipo de mantenimiento:");
                Console.WriteLine("1. Preventivo");
                Console.WriteLine("2. Correctivo");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcionTipo))
                {
                    opcionTipo = 0;
                }

                if (opcionTipo != 1 && opcionTipo != 2)
                {
                    Console.WriteLine("ERROR: Seleccione 1 o 2.");
                }

            } while (opcionTipo != 1 && opcionTipo != 2);

            string tipo;

            if (opcionTipo == 1)
            {
                tipo = "Preventivo";
            }
            else
            {
                tipo = "Correctivo";
            }

            double costo;

            do
            {
                Console.Write("Ingrese costo del mantenimiento: ");

                if (!double.TryParse(Console.ReadLine(), out costo) || costo < 0)
                {
                    Console.WriteLine("ERROR: El costo no puede ser negativo.");
                    costo = -1;
                }

            } while (costo < 0);

            codigoMantenimiento[totalMantenimientos] = codigo;
            fechaMantenimiento[totalMantenimientos] = fecha;
            tipoMantenimiento[totalMantenimientos] = tipo;
            costoMantenimiento[totalMantenimientos] = costo;

            totalMantenimientos++;

            GuardarMantenimientos();

            Console.WriteLine("\n¡Mantenimiento registrado correctamente!");
        }

        static void MostrarEncabezado()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║        SISTEMA DE MANTENIMIENTO         ║");
            Console.WriteLine("║             DE MAQUINARIA               ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");

            Console.ResetColor();
        }

        static void ListarMaquinaria()
        {
            if (totalMaquinas == 0)
            {
                Console.WriteLine("No hay máquinas registradas.");
                return;
            }

            Console.WriteLine("\n--- LISTA DE MAQUINARIA ---");

            for (int i = 0; i < totalMaquinas; i++)
            {
                string estado;

                if (horasUso[i] >= frecuenciaMantenimiento[i])
                {
                    estado = "REQUIERE MANT.";
                }
                else
                {
                    estado = "AL DIA";
                }

                string ultimaFecha = "Sin mantenimiento";

                for (int j = 0; j < totalMantenimientos; j++)
                {
                    if (codigoMantenimiento[j] == codigosMaquina[i])
                    {
                        ultimaFecha = fechaMantenimiento[j];
                    }
                }

                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Código: " + codigosMaquina[i]);
                Console.WriteLine("Nombre: " + nombresMaquina[i]);
                Console.WriteLine("Horas de uso: " + horasUso[i]);
                Console.WriteLine("Frecuencia: " + frecuenciaMantenimiento[i]);
                Console.WriteLine("Estado: " + estado);
                Console.WriteLine("Última fecha de mantenimiento: " + ultimaFecha);
            }
        }

        static void MaquinasQueRequierenMantenimiento()
        {
            if (totalMaquinas == 0)
            {
                Console.WriteLine("No hay máquinas registradas.");
                return;
            }

            bool encontrada = false;

            Console.WriteLine("\n--- MÁQUINAS QUE REQUIEREN MANTENIMIENTO ---");

            for (int i = 0; i < totalMaquinas; i++)
            {
                if (horasUso[i] >= frecuenciaMantenimiento[i])
                {
                    encontrada = true;

                    string ultimaFecha = "Sin mantenimiento";
                    int vecesMantenimiento = 0;

                    for (int j = 0; j < totalMantenimientos; j++)
                    {
                        if (codigoMantenimiento[j] == codigosMaquina[i])
                        {
                            ultimaFecha = fechaMantenimiento[j];
                            vecesMantenimiento++;
                        }
                    }

                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Código: " + codigosMaquina[i]);
                    Console.WriteLine("Máquina: " + nombresMaquina[i]);
                    Console.WriteLine("Última fecha de mantenimiento: " + ultimaFecha);
                    Console.WriteLine("Veces que recibió mantenimiento: " + vecesMantenimiento);
                }
            }

            if (!encontrada)
            {
                Console.WriteLine("No hay máquinas que requieran mantenimiento.");
            }
        }

        static void CostoTotalPorMaquina()
        {
            if (totalMaquinas == 0)
            {
                Console.WriteLine("No hay máquinas registradas.");
                return;
            }

            Console.WriteLine("\n--- COSTO TOTAL DE MANTENIMIENTO POR MÁQUINA ---");

            for (int i = 0; i < totalMaquinas; i++)
            {
                double totalCosto = 0;

                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Código: " + codigosMaquina[i]);
                Console.WriteLine("Máquina: " + nombresMaquina[i]);

                for (int j = 0; j < totalMantenimientos; j++)
                {
                    if (codigoMantenimiento[j] == codigosMaquina[i])
                    {
                        Console.WriteLine("Fecha: " + fechaMantenimiento[j]);
                        Console.WriteLine("Costo: " + costoMantenimiento[j] + " Bs");

                        totalCosto = totalCosto + costoMantenimiento[j];
                    }
                }

                Console.WriteLine("Costo total: " + totalCosto + " Bs");
            }
        }

        // ========================================
        // BLOQUE 4: ARCHIVOS TXT
        // ========================================

        static void GuardarMaquinas()
        {
            using (StreamWriter escritor = new StreamWriter(ARCHIVO_MAQUINAS))
            {
                for (int i = 0; i < totalMaquinas; i++)
                {
                    escritor.WriteLine(
                        codigosMaquina[i] + "|" +
                        nombresMaquina[i] + "|" +
                        horasUso[i] + "|" +
                        frecuenciaMantenimiento[i]);
                }
            }
        }

        static void GuardarMantenimientos()
        {
            using (StreamWriter escritor = new StreamWriter(ARCHIVO_MANTENIMIENTOS))
            {
                for (int i = 0; i < totalMantenimientos; i++)
                {
                    escritor.WriteLine(
                        codigoMantenimiento[i] + "|" +
                        fechaMantenimiento[i] + "|" +
                        tipoMantenimiento[i] + "|" +
                        costoMantenimiento[i]);
                }
            }
        }

        static void GenerarReporteTXT()
        {
            string archivo = "reporte_mantenimiento.txt";

            using (StreamWriter escritor = new StreamWriter(archivo))
            {
                escritor.WriteLine("==============================================");
                escritor.WriteLine(" REPORTE DE MANTENIMIENTO DE MAQUINARIA");
                escritor.WriteLine("==============================================");
                escritor.WriteLine();

                escritor.WriteLine("TOTAL DE MAQUINAS: " + totalMaquinas);
                escritor.WriteLine("TOTAL DE MANTENIMIENTOS: " + totalMantenimientos);
                escritor.WriteLine();

                escritor.WriteLine("--- MAQUINARIA ---");
                escritor.WriteLine();

                for (int i = 0; i < totalMaquinas; i++)
                {
                    string estado;

                    if (horasUso[i] >= frecuenciaMantenimiento[i])
                    {
                        estado = "REQUIERE MANTENIMIENTO";
                    }
                    else
                    {
                        estado = "AL DIA";
                    }

                    escritor.WriteLine("Código: " + codigosMaquina[i]);
                    escritor.WriteLine("Nombre: " + nombresMaquina[i]);
                    escritor.WriteLine("Horas de uso: " + horasUso[i]);
                    escritor.WriteLine("Frecuencia: " + frecuenciaMantenimiento[i]);
                    escritor.WriteLine("Estado: " + estado);
                    escritor.WriteLine("----------------------------------------------");
                }

                escritor.WriteLine();
                escritor.WriteLine("--- MANTENIMIENTOS REALIZADOS ---");
                escritor.WriteLine();

                for (int i = 0; i < totalMantenimientos; i++)
                {
                    escritor.WriteLine("Código máquina: " + codigoMantenimiento[i]);
                    escritor.WriteLine("Fecha: " + fechaMantenimiento[i]);
                    escritor.WriteLine("Tipo: " + tipoMantenimiento[i]);
                    escritor.WriteLine("Costo: " + costoMantenimiento[i] + " Bs");
                    escritor.WriteLine("----------------------------------------------");
                }
            }

            Console.WriteLine("\nReporte generado correctamente.");
            Console.WriteLine("Archivo: " + archivo);
        }

        static void CargarMaquinas()
        {
            if (!File.Exists(ARCHIVO_MAQUINAS))
            {
                return;
            }

            string[] lineas = File.ReadAllLines(ARCHIVO_MAQUINAS);

            foreach (string linea in lineas)
            {
                if (totalMaquinas >= MAX_REGISTROS)
                {
                    break;
                }

                string[] datos = linea.Split('|');

                if (datos.Length >= 4)
                {
                    codigosMaquina[totalMaquinas] = datos[0];
                    nombresMaquina[totalMaquinas] = datos[1];
                    horasUso[totalMaquinas] = double.Parse(datos[2]);
                    frecuenciaMantenimiento[totalMaquinas] = double.Parse(datos[3]);

                    totalMaquinas++;
                }
            }
        }

        static void CargarMantenimientos()
        {
            if (!File.Exists(ARCHIVO_MANTENIMIENTOS))
            {
                return;
            }

            string[] lineas = File.ReadAllLines(ARCHIVO_MANTENIMIENTOS);

            foreach (string linea in lineas)
            {
                if (totalMantenimientos >= MAX_REGISTROS)
                {
                    break;
                }

                string[] datos = linea.Split('|');

                if (datos.Length >= 4)
                {
                    codigoMantenimiento[totalMantenimientos] = datos[0];
                    fechaMantenimiento[totalMantenimientos] = datos[1];
                    tipoMantenimiento[totalMantenimientos] = datos[2];
                    costoMantenimiento[totalMantenimientos] = double.Parse(datos[3]);

                    totalMantenimientos++;
                }
            }
        }
    }
}

