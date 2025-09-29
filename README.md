Arquitectura de la aplicacion

La solucion se desarrollo en .NET 8 con base de datos SQL server.

La solucion consta de los siguientes proyectos:
- RealStateDataModel --> Libreria de clases para el manejo de datos
- RealStateService   --> Libreria de clases para alojar los servicios necesarios
- RealStateApi       --> Web API para alojar los endpoints necesarios
       - api/TbProperties: Lista todas las propiedades
       - api/TbProperties/GetFilteredTbProperties: Lista las propiedades con filtros
       - api/TbProperties/UpdateProperty: Actualiza la propiedad
       - api/TbProperties/ChangePrice: Actualiza el precoi de la propiedad
       - api/TbProperties/CreateProperty: Crea una nueva propiedad
       - api/TbProperties/UploadImage: Agrega imagenes a la propiedad
- TechnicalTestPhanorMesias --> Aplicacion web MVC para crear una interfaz de usuario funcional. (Se encuentra publicada en http://realstatetest.somee.com)
  
  El backup de la base de datos s eencuentra en la ruta: https://github.com/phadi/TechnicalTestPhanorMesias/tree/master/RealStateDataModel/Backup.

  Para ejectutar el proyecto API, solo se debe abrir la solucion completa en Visual Stidio, seleccionar el proyecto RealStateApi como proyecto de inicio y ejecutar.

   Para ejectutar el proyecto MVC, solo se debe abrir la solucion completa en Visual Stidio, seleccionar el proyecto TechnicalTestPhanorMesias como proyecto de inicio y ejecutar.
