**SISTEMA DE MANTENIMIENTO PREVENTIVO DE MAQUINARIA**

**Asignatura**: Programación Numérica y Aplicaciones

**Carrera**: Ingeniería Industrial -- 4to Semestre

**Gestion**: 2026

**Docente**: Lic. Andrés Grover Albino Chambi

# Equipo de Trabajo

  -----------------------------------------------------------------------
  **Integrante**                      **Rol en el Proyecto**
  ----------------------------------- -----------------------------------
  Choquehuanca Laruta Yubitza         Desarrollo y documentación

  Solá Guzman Fabiana Valentina       Desarrollo y pruebas

  Urteaga Hinojosa Adriana Lorena     Desarrollo y documentación
  -----------------------------------------------------------------------

# Descripción del Proyecto

**Contexto.** Una planta necesita gestionar el mantenimiento preventivo
de su maquinaria para prolongar su vida útil y evitar paradas no
programadas. El proyecto desarrolla una aplicación de consola que
permite registrar maquinaria, controlar sus horas de uso y frecuencia de
mantenimiento, registrar mantenimientos realizados y consultar el estado
de cada máquina.

**Eje transversal -- ODS 12: Producción y Consumo Responsables**

El ODS 12 significa Objetivo de Desarrollo Sostenible 12: Producción y
Consumo Responsables. Este objetivo busca que las empresas utilicen de
manera adecuada sus recursos y eviten desperdicios innecesarios. En
nuestro proyecto se relaciona con el mantenimiento preventivo de
maquinaria, ya que realizar controles y mantenimientos a tiempo ayuda a
conservar las máquinas en buenas condiciones, alargar su vida útil y
evitar daños o fallas que puedan causar paradas inesperadas. De esta
manera, se puede aprovechar mejor la maquinaria, reducir gastos
innecesarios y utilizar los recursos de la planta de una forma más
responsable.

# Objetivo General

# Desarrollar un sistema de mantenimiento preventivo para controlar el estado de la maquinaria y detectar cuándo necesita mantenimiento.

# Objetivos Específicos

**1.** Digitalizar el registro de maquinaria mediante código, nombre,
horas de uso acumuladas y frecuencia de mantenimiento.

**2.** Registrar los mantenimientos realizados indicando código de
máquina, fecha, tipo (preventivo/correctivo) y costo.

**3.** Identificar automáticamente las máquinas que requieren
mantenimiento cuando las horas de uso alcanzan o superan su frecuencia
establecida.

**4.** Calcular el costo total de mantenimiento por máquina.

**5.** Generar y conservar reportes generales en archivos de texto
(.txt).

# Funcionalidades Principales

El menú principal permite:

1)  Registrar maquinaria,

2)  Registrar mantenimiento,

3)  Listar maquinaria y su estado,

4)  Mostrar máquinas que requieren mantenimiento,

5)  Consultar el costo total por máquina,

6)  Generar un reporte TXT

7)  Salir. El programa valida códigos repetidos, valores numéricos

8)  Tipos de mantenimiento.

La información se almacena en los archivos **maquinas.txt** y
**mantenimientos.txt**, utilizando el separador "\|".

# Tecnologías Utilizadas

  -----------------------------------------------------------------------
  **Lenguaje**                        C# 7.3 (.NET Framework)
  ----------------------------------- -----------------------------------
  **Entorno**                         Visual Studio 2019/2022 --
                                      Aplicación de Consola

  **Persistencia**                    Archivos de texto plano (.txt) con
                                      separador \|

  **Estructuras utilizadas**          Arreglos paralelos, métodos,
                                      ciclos, condicionales y
                                      validaciones

  **Control de versiones**            Git y GitHub
  -----------------------------------------------------------------------

# Instalación y Ejecución

**1.** Abrir el proyecto en Visual Studio 2019.

**2.** Verificar que el proyecto corresponda a una aplicación de consola
compatible con C# 7.3/.NET Framework.

**3.** Compilar la solución para comprobar que no existan errores.

**4.** Ejecutar el programa. Al iniciar, el sistema carga los archivos
maquinas.txt y mantenimientos.txt si existen.

**5.** Utilizar el menú principal para registrar información y generar
el archivo reporte_mantenimiento.txt.

**6.** Para trabajar mediante repositorio, clonar el Proyecto con:
<https://github.com/hinojosalorena094/proyecto-5.git>

**7.** <https://youtu.be/QkqEsuIYNQU>

# Resultado Esperado

El sistema permite tener toda la información de mantenimiento en un solo
lugar de la maquinaria, identificar oportunamente las máquinas que
requieren atención, consultar los costos acumulados y generar un reporte
en formato TXT para ayudar al control y a la toma de decisiones en la
planta.
