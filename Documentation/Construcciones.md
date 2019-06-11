# Construcciones



## Introducción


El proyecto de Construcciones consta en poder filmar un plano de construcción ya predeterminado y, con realidad aumentada (RA) poder mostrar virtualmente las paredes y las columnas de dicha construcción, pudiendo visualizar gráficamente la intensidad de las cargas aplicadas a lo largo de las vías.



## Usabilidad


_[En esta sección se pretende mostrar la aplicación a través de texto, videos e imágenes, explicando los botones y demás. Completar cuando se tenga algo contundente]_



## Estructura del proyecto


Dentro de la jerarquía de la escena de _Construcciones_ se pueden observar cinco objetos. Ellos son: Directional Light, ARCamera, ImageTarget, Canvas y EventSystem.  


**Directional Light** contiene una luz direccional para iluminar la escena. Los detalles de la misma no tienen mayor relevancia. **EventSystem** es un objeto propio de Unity dedicado a manejar la escena. Los detalles del mismo se especifican mejor en la [documentación de Unity](https://docs.unity3d.com/ScriptReference/EventSystems.EventSystem.html). **ARCamera** es el objecto que contiene el componente de la cámara de la escena (notar que la cámara de la escena no es la cámara fotográfica del dispositivo). Esta cámara tiene a su vez el script _Vuforia Behaviour_ que se encarga de mostrar la cámara del dispositivo y detectar automáticamente las imágenes (Image Targets). Vale aclarar que todo esto lo maneja Vuforia, y que de parte del desarrollador no resta más que crearlo y hacer las configuraciones mínimas.  


Los objetos restantes, **ImageTarget** y **Canvas** se describen en las subsecciones siguientes



### ImageTarget

Los objectos ImageTarget de Vuforia son los que contienen la imagen a escanear y también los objetos virtuales a mostrar. Los componentes que estos objetos incluyen ya vienen predeterminadamente configurados por Vuforia. Sólo hay que configurar el componente 'Image Target Behaviour', especificando la base de datos de Vuforia y la textura a capturar.

Todos los objetos virtuales que se renderizarán al encontrar el plano deben ser hijos de ImageTarget, y con eso es suficiente. Los scripts de Vuforia (especificamente DefaultTrackableEventHandler.cs) se encargarán de mostrarlos cuando se detecte el plano. Específicamente para este proyecto, tenemos dos grupos principales de objetos virtuales: **Cargas** y **Edificio**. El objeto Edificio contiene las paredes, columnas y el techo que se renderiza. Dado que las paredes se tornan 50% transparentes al mostrar las cargas es necesario que el material que las compone	tenga setteado el modo de rendering en _Transparente_.  

El objeto Cargas contiene las gráficas de las cargas que cada viga soporta a lo largo de la misma. Estas gráficas no son más que sprites en .png colocadas en la escena de una manera predeterminada.



### Canvas

El objeto Canvas contiene la UI de la aplicación. Esto incluye el botón FocusButton, la barra lateral SideBar y el objeto CambioDeEscenas. El objeto **FocusButton** maneja el foco de la cámara del dispositivo para poder enfocar mejor el plano físico. Se implementó especialmente para casos donde la cámara del dispositivo no sea muy buena.  

La barra lateral **Side Bar** incluye tres botones: BotonAumentos, BotonVolver y BotonCargas. El primero permite navegar entre proyectos (como ser SistemaPlanetario, Peralte o Construcciones mismo). La lógica del cambio de escenas se encuentra en el objeto CambioDeEscenas. El botón **BotonCargas** permite mostrar u ocultar las cargas de las vigas en la escena. El script que maneja su lógica es ShowLoads.cs y se encuentra en `Assets/Custom/Scripts/ShowLoads.cs`