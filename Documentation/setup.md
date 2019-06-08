# Instalación del entorno

## Sistema operativo
* Windows
* MacOS

En este proyecto trabajamos con Vuforia, el motor de realidad aumentada que usamos. Dado que tiene soporte únicamente para Windows y MacIOs no se puede trabajar en un sistema operativo de tipo Linux.

## Programas

Para poder trabajar sobre este proyecto  necesitamos tener en nuestro local los siguientes programas:
* **Unity**:

  [Unity](https://unity3d.com/es/get-unity/download) es una plataforma de desarrollo 3D. Es uno de los principales programas disponibles en el mercado para este tipo de desarrollos (principalmente de videojuegos o en nuestro caso de realidad aumentada).
Es a partir de Unity que se generan los archivos ejecutables.

* **Blender**:

  [Blender](https://www.blender.org/download/) es un programa para modelado de objetos 3D, creación de animaciones y demás (como son Maya o 3DsMax). Es gratuito, de código abierto, muy liviano y muy versátil. Es muchísimo más cómodo modelar un objeto en Blender que en Unity, de ahí la razón por su uso. Además es muy compatible con Unity dado que para importar un proyecto .blend en Unity solo hay que hacer un drag and drop.

* **Vuforia**:
  * [Vuforia Model Target Generator](https://developer.vuforia.com/downloads/tool) (Programa)
  * [Vuforia](https://developer.vuforia.com/) (plugin para Unity)

  *Vuforia* es el motor de realidad aumentada. Es el software que se ocupa de identificar un objeto o una imagen en el espacio y disparar el aumento (es decir los objetos y animaciones programadas por nosotros sobre el objeto). Es un software privativo, gratuito hasta cierto punto con unas restricciones mucho más estrictas que las de Unity sobre las que volveremos a hablar más adelante.

  El *Vuforia Model Target Generator* es una herramienta provista por Vuforia para generar lo que se conoce como un Model Target: el objeto objetivo que va a estar buscando para disparar la animación. Se requiere en la etapa final del proyecto, cuando ya estamos generando el archivo ejecutable y necesitamos darle a Vuforia la información necesaria sobre el objeto a capturar.

* **Android SDK** o **Android Studio**

  Dado que construiremos APK’s para tablets y celulares con Android, conviene además instalar el [Android Studio](https://developer.android.com/studio) ya que provee todo lo necesario para hacer este tipo de buildings. Se puede tratar de instalar el sdk de Android solamente, pero esto puede inducir complicaciones y por esto es que recomendamos directamente cortar por lo sano e instalar el Android Studio.

Además a modo accesorio recomendamos instalar:
* **Gimp**:

  [Gimp](https://www.gimp.org/downloads/) es un editor de imágenes de código abierto, suele ser bastante útil tenerlo a mano cuando se está desarrollando un proyecto que requiere elementos en 2D, pero no es fundamental tenerlo instalado.
* **Visual Studio**

  El visual studio es un IDE gratuito. Se puede usar cualquier otro; el Rider de JetBrains es bastante bueno también y se puede configurar Unity para usar ese, pero Visual Studio es fácilmente instalable dado que en la instalación de Unity te ofrece descargarlo e instalarlo.

## Sistema de control de versiones

Hay 2 formas:
* **Collab**

  Es una herramienta que viene integrada a Unity y permite constituir equipos de no más de 3 desarrolladores, debiéndose pagar una cuota en dólares una vez superado ese límite.
  Estuvimos usando durante un tiempo esta herramienta pero es bastante poco recomendable por ser incómoda dado que hay un único branch sobre el que trabajar y tiene además el límite de 3 desarrolladores mencionado.
  
* **Git**:

  Es el programa que estamos usando en la actualidad.
Hay que notar que Unity es un programa que por atrás genera muchísimos archivos de tipo .yml y otros tipos más, por lo que hay que tener cuidado con qué se puede ignorar. El archivo .gitignore que tenemos configurado está funcionando bien.
