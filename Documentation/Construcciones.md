# Construcciones


## Introducción

El proyecto de Construcciones consta en poder filmar un plano de construcción ya predeterminado y, con realidad aumentada (RA) poder mostrar virtualmente los diferentes componentes de la construcción, pudiendo visualizar gráficamente cómo las cargas de las losas se distribuyen a lo largo de las vigas y las columnas.


## Usabilidad

La funcionalidad de esta aplicación se limita a aumentar el edificio sobre la hoja de papel y mostrar cómo se distribuyen las cargas de las losas por las columnas y vigas. Toda esta información se presenta en forma de slides (o diapositivas) que se van controlando con dos botones (para ir a la siguiente o a la anterior). Dada la reducida complejidad la interfaz de usuario es muy sencilla. A continuación se ilustra una captura de pantalla y se explican los botones presentes

<img src="Images/construcciones_init.jpg">

En el panel de la izquierda se pueden observar dos botones: el de proyectos y el de salida. Como todas las aplicaciones del proyecto UBATIC, el botón de **proyectos** permite navegar entre otros proyectos (Sistema Planetario y Peralte). El botón de **salida** permite cerrar la aplicación. Además, abajo a la derecha se encuentra el botón de **foco**, el cual permite enfocar la imagen con mejor calidad. Es especialmente útil para dispositivos cuyas cámaras no sean de gama alta.

Las slides que la aplicación contiene visualizan:

- La estructura externa del edificio
- Las columnas y vigas
- La carga de las losas sobre las columnas
- La distribución de las cargas de las losas a lo largo de las columnas
- Visualización de las paredes y los puntos de presión sobre las columnas
- Los gráficos de carga en función de la posición de cada viga

<img src="Images/construcciones_edificio_ext.jpg">

<img src="Images/construcciones_cargas_columnas.jpg">

<img src="Images/construcciones_cargas_vigas.jpg">


## Estructura del proyecto

Dentro de la jerarquía de la escena de _Construcciones_ se pueden observar dos luces, ARCamera, ImageTarget, Canvas, SlidesController y EventSystem.  

**EventSystem** es un objeto propio de Unity dedicado a manejar la escena. Los detalles del mismo se especifican mejor en la [documentación de Unity](https://docs.unity3d.com/ScriptReference/EventSystems.EventSystem.html).

**ARCamera** es el objecto que contiene el componente de la cámara de la escena (notar que la cámara de la escena no es exactamente la cámara fotográfica del dispositivo). Esta cámara tiene a su vez el script _Vuforia Behaviour_ que se encarga de mostrar la cámara del dispositivo y detectar automáticamente las imágenes (Image Targets). Vale aclarar que todo esto lo maneja Vuforia, y que de parte del desarrollador no resta más que crearlo y hacer las configuraciones mínimas.  

Los objetos restantes se describen en las subsecciones siguientes

### SlidesController  

Este objeto no es más que un GameObject vacío con el script SlidesController.cs que contiene la lógica para controlar las slides. Contiene una referencia a todos los objetos que se muestran en el aumento, con los cuales arma los slides. Además, crea una lista de **ConstructionSlide** e implementa los métodos NextSlide() y PreviousSlide(). A su vez, el objeto ConstructionSlide tiene una colección de GameObject's a mostrar e implementa los métodos Hide() y Show() para ocultar o mostrar todos los objetos del slide.

### ImageTarget

Los objectos ImageTarget de Vuforia son los que contienen la imagen a escanear y también los objetos virtuales a mostrar. Los componentes que estos objetos incluyen ya vienen predeterminadamente configurados por Vuforia. Sólo hay que configurar el componente 'Image Target Behaviour', especificando la base de datos de Vuforia y la textura a capturar.

Todos los objetos virtuales que se renderizarán al encontrar el plano deben ser hijos de ImageTarget, y con eso es suficiente. Los Image Targets contienen un script encargado de mostrar los objetos virtuales cuando se detecte el plano. Predeterminadamente Vuforia ofrece el script DefaultTrackableEventHandler.cs que se encarga de esto. En este proyecto se tiene un BlueprintTrackableEventHandler.cs que hereda de DefaultTrackableEventHandler y, además, muestra y esconde los botones para cambiar de slides cuando se detecta o se deja de detectar el plano, respectivamente.

### Canvas

El objeto Canvas contiene la UI de la aplicación. Esto incluye el botón FocusButton, la barra lateral SideBar, el objeto CambioDeEscenas y los botones para cambiar de slides. El objeto **FocusButton** maneja el foco de la cámara del dispositivo para poder enfocar mejor el plano físico. Se implementó especialmente para casos donde la cámara del dispositivo no sea muy buena.  

La barra lateral **Side Bar** incluye dos botones: BotonAumentos y BotonVolver. El primero permite navegar entre proyectos (como ser SistemaPlanetario, Peralte o Construcciones mismo). La lógica del cambio de escenas se encuentra en el objeto CambioDeEscenas.
