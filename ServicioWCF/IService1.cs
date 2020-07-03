using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ClienteWPF;

namespace ServicioWCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        Ficha[][] IniciarTablero(Ficha[][] matriz, Ficha fichaVacia);

        [OperationContract]
        Tablero ProbarTablero(Tablero tablero);

        [OperationContract]
        int PiezasEnColumna(Ficha[][] tablero, int columna, Ficha fichaVacia);

        [OperationContract]
        bool VerificarLugar(Ficha[][] tablero, int columna, Ficha fichaVacia, Jugador jugadorActual);

        [OperationContract]
        Ficha[][] Colocar(Ficha[][] tablero, int columna, Ficha fichaVacia, Jugador jugadorActual);

        [OperationContract]
        Jugador CambiarTurnoJugador(Jugador jugador1, Jugador jugador2, Jugador jugadorActual);

        [OperationContract]
        string FinDeTurno(Ficha[][] tablero, Ficha fichaVacia, Jugador jugadorActual);

        // TODO: agregue aquí sus operaciones de servicio
    }

    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    // Puede agregar archivos XSD al proyecto. Después de compilar el proyecto, puede usar directamente los tipos de datos definidos aquí, con el espacio de nombres "ServicioWCF.ContractType".
    /*[DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }*/
    
    [DataContract]
    public class Tablero
    {
        Ficha[][] matriz;
        int filas;
        int columnas;

        [DataMember]
        public Ficha[][] Matriz
        {
            get { return matriz; }
            set { matriz = value; }
        }

        [DataMember]

        public int Filas
        {
            get { return filas; }
            set { filas = value; }
        }

        [DataMember]
        public int Columnas
        {
            get { return columnas; }
            set { columnas = value; }
        }
    }
    
    
    [DataContract]
    public class Ficha
    {
        int id;
        string color;

        [DataMember]
        public int Id 
        {
            get { return id; }
            set { id = value; } 
        }

        [DataMember]
        public string Color 
        { 
            get { return color; }
            set { color = value; } 
        }
    }

    [DataContract]
    public class Jugador
    {
        int id;
        string nombre;
        Ficha ficha;

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        [DataMember]
        public Ficha Ficha
        {
            get { return ficha; }
            set { ficha = value; }
        }

    }
    
}
