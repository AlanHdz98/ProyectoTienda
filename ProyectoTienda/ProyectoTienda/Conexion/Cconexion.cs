using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;

namespace ProyectoTienda.Conexion
{
    public class Cconexion
    {
        public static FirebaseClient firebase = new FirebaseClient("https://appcompras-53174-default-rtdb.firebaseio.com/");
    }
}
