# ZizZagZio

## Instrucciones de juego

### PC



### MÓVIL



## SUT!  - Documento de Diseño


## 1. Introducción

A continuación se presenta el documento de diseño de **Neon Rush**, videojuego desarrollado en Unity como práctica final de la asignatura _Juegos Para Web y Redes Sociales_ del _Grado de Diseño y Desarrollo de Videojuegos_ de la _Universidad Rey Juan Carlos_.

- Puedes jugar  a Neon Rush haciendo click en este enlace:

- Puedes acceder a nuestra web mediante este enlace: https://grupo9web.github.io/WebHulioEntertainment/

- Consulta nuestro repositorio en GitHub: https://github.com/grupo9web/ZizZagZio

### 1.1 Concepto del juego

Neon Rush es un videojuego 3D del género arcade que posee la jugabilidad clásica de los juegos infinite runner. El jugador esta representado por una esfera que avanza constantemente hacia delante o hacia la izquierda y nuestro objetivo será aguantar el máximo tiempo posible.

### 1.2 Características principales. 


El juego se basa en 3 principios básicos:

- **Mecánica sencilla:** la mecánica del juego es muy simple, el jugador tendrá que conseguir sobrevivir el máximo tiempo posible sin caerse del recorrido, la velocidad del jugador irá aumentando progresivamente para hacer la tarea mas compleja.
- **Juego infinito:** A medida que consigamos mantenernos sobre el camino el jugo irá generando piezas por las que poder avanzar.
- **Dificultad progresiva:** Puesto que el juego es infinito, el objetivo será aguantar el máximo tiempo posible, para hacer esta tarea más compleja se irá aumentando la velocidad de manera progresiva e intentar hacer caer al jugador con power-ups. 


### 1.3 Género

Se podría encasillar en un juego perteneciente al género de plataformas con una estética arcade.


### 1.4 Propósito y público objetivo

El propósito de Neon Rush consiste en hacer entretener a los usuarios en cualquier rato de ocio disponible, que busquén una experiencia corta pero frenética. El juego está destinado a todo tipo de público indiferentemente de su sexo o edad. 


## 2. Mecánicas del juego

La mecánica principal de Neon Rush es deslizarte a través de las plataformas que se van generando sin caerte. Neon Rush es un juego infinito con dificultad incremental, por lo que el objetivo es aguantar lo máximo posible. 
Para moverse por estas plataformas, el jugador podrá realizar un cambio de dirección entre ir al frente o hacia la izquierda.

Durante la partida podrás usar a tu favor 2 tipos de power ups. El primer power up es un salto que te permitirá saltar varias piezas de golpe. El segundo power up beneficioso es una reducción temporal de velocidad, lo que te permitirá realizar los giros con mayor seguridad. En otro lugar, en Neon Rush se ha incluido un power up perjudicial para el jugador. Este power up consiste en una reducción temporal del area de visión, dificultado la partida del jugador.

Para dotar de mayor dinamismo al juego, se han incluido cambios de eje en el mundo. Estos cambios de eje se producirán despues de la aparición de una cierta cantidad de piezas. Para evitar un fin de partida repentino para el jugador en estos cambios de eje, se reduce la velocidad del juego.

Para realizar la animación de salto hemos empleado un tutorial que nos permitia definir el movimiento a través de una parábola. 
Para que funcionase en todos los ejes por los que se puede mover la bola tuvimos que modificar el código para contemplar esos casos. Haciendo click en la imagen de abajo os llevará al tutorial.

[![Parabola](http://img.youtube.com/vi/ddakS7BgHRI/0.jpg)](https://www.youtube.com/watch?v=ddakS7BgHRI "Moving Object Along A Parabola | Unity Tutorial 🎓")
### 2.1 Cámara

El juego sera en 3D con una cámara en ángulo picado. Estará centrada en el personaje y lo seguirá. Al realizar el jugador la acción de cambio de dirección, la cámara tambien girará con él, de forma que desde la vista del jugador, siempre se avanzará hacia el frente

#### Frontal 
![Lateral](https://i.imgur.com/jyyyG1a.jpg)
### Lateral
![Lateral](https://i.imgur.com/TBrxaqV.jpg)

#### 2.1.2 Periféricos y controles

Para cambiar la dirección a la que va la pelota, el jugador simplemente tendrá que hacer click izq si está jugando en un ordenador o un toque en la pantalla si está jugando en un dispositivo portatil o tactil. Con esta estrategia buscabamos que el control fuese lo más sencillo posible y que se adaptase a cualquier dispositivo

### 2.2 Puntuación

La puntuación de la partida se basará en el numero de piezas superadas, sumando 10 por cada una de ellas. El objetivo principal del juego es conseguir la maxima puntuación posible.

### 2.3 Guardar/Cargar

Usando playerpers de Unity se guardan las 10 mejores puntuaciones del navegador con el que juegues a Neon Rush. Para ello hemos incluido una tabla de puntuaciones accesible a través del menu principal en la opcion marcadores.

## 2.5 Personajes

En Neon Rush no existen personajes como tal, el jugador es simplemente representado por una bola.

## 3. Interfaz

En este apartado se explicarán los diferentes __flujos de actividades__ que componen la interfaz del videojuego *Neon Rush*, así como una ejemplificación de algunas de las secciones por medio de __imágenes__.

### 3.1 Diagrama de flujo

El siguiente diagrama de flujo describe los estados en los que se puede encontrar nuestro juego *Neon Rush* y las posibles transiciones entre los mismos según las acciones del usuario.

![Error en carga de Diagrama de Flujo](https://i.imgur.com/QREcMHD.png)

A continuación se muestra también un diagrama de estados adicional que describe las posibilidades y situaciones que pueden suceder durante el estado de **Juego**:

![Error en carga de Diagrama de Flujo, Juego](https://i.imgur.com/VbgazgT.png)


### 3.2 Menú de Selección de Nombre

Si el usuario abre por primera vez el juego o tiene un nombre aleatorio le aparecerá esta pantalla. La idea es que sirva para dar la bienvenida y como método de introducción al juego, permitiendo al jugador escoger un nombre personalizado.

![Error en carga de Selector de Nombre](https://i.imgur.com/fHoZtTa.png)


- **1. Introducir nombre**: espacio en blanco para que el usuario introduzca el nombre con el que quiere aparecer reflejado en el juego. En caso de dejarlo vacío se le asignará un nombre aleatorio.
- **2.  Botón de continuar**: da acceso al Menú Principal. Si el campo anterior está vacío se le preguntará al usuario si realmente no quiere introducir un nombre.


### 3.3 Menú Principal

En caso de que el usuario haya escogido nombre o rechace el elegir uno se le llevará al Menú Principal, que finalmente tiene esta estética:

![Error en carga de Menu Principal](https://i.imgur.com/cCcSFwU.png)
- **1. Nueva Partida**: permite al usuario iniciar una nueva partida.
- **2. Ajustes**: acceso al menú de ajustes, donde se pueden cambiar algunas opciones de juego.
- **3. Marcadores**: muestra al usuario el nombre y puntuación de los 10 mejores jugadores que han abierto el juego en el navegador.
- **4. Créditos**: da visibilidad a los desarrolladores del videojuego, permitiendo acceder al github de cada uno de ellos.

### 3.4 Pantalla de Juego

Si el jugador decide empezar una partida tendrá la siguiente información en pantalla en cuanto a interfaz se refiere:

![Error en carga de Pantalla de Juego](https://i.imgur.com/a1NXmqN.png)

- **1. Puntuación**: informa al usuario de la puntuación que lleva en la partida.
- **2. Botón de Pausa**:  da la posibilidad de detener el juego y cambiar el volumen, continuar o volver al Menú Principal.
 

### 3.5 Game Over

Cuando el usuario cae por alguno de los lados intentando avanzar la partida llegará a su fin y se abrirá el siguiente menú:

![Error en carga de Game Over](https://i.imgur.com/CpMSodT.png)

- **1. Puntuación**: permite ver la puntuación final obtenida en la partida.
- **2. Top**: da al usuario la información del mejor jugador.
- **3. Reintentar**: vuelve a cargar la escena y reinicia la partida.
- **4. Menú Principal**: devuelve al usuario al Menú Principal.
 
### 3.6 Menú de Ajustes

Accesible desde el Menú Principal, permite cambiar ajustes de juego para que el jugador se encuentre lo más cómodo posible:

![Error en carga de Menú de Ajustes](https://i.imgur.com/BIJwiwv.png)

- **1. Botones de Idioma**: permite al usuario cambiar entre los idiomas disponibles (español, inglés y francés).
- **2. Barra de Volumen**: habilita la posibilidad de cambiar el volumen de los sonidos del juego.
- **3. Botón de Cambiar el Nombre**: acceso al Menú de Cambiar Nombre.
- **4. Volver**: forma de acceder al Menú Principal.
 
### 3.7 Marcadores

Este menú tiene la siguiente estética:

![Error en carga de Marcadores](https://i.imgur.com/aSKAzh7.png)

- **1. Lista de jugadores**: proporciona una lista ordenada de mayor a menor puntuación de los 10 mejores jugadores.
- **2. Volver**: acceso al menú principal.

### 3.8 Créditos

Finalmente se reserva un apartado para dar créditos a los autores:

![Error en carga de Créditos](https://i.imgur.com/dKK2OQd.png)

- **1. Lista de desarrolladores**: proporciona al usuario la lista de los desarrolladores del videojuego.
- **2. Botón de Github**: permite acceder a los Githubs de los desarrolladores.
- **3. Volver**: acceso al menú principal.

## 4. Arte y Música

### 4.1 Arte

##### 4.1.1 Interfaz

Los elementos de la interfaz son completamente originales, inspirados en la estética conocida como _"Vaporwave"_ junto a la tendencia de usar neones en zonas de _Japón_, las cuales se han considerado idóneas para incluir en el proyecto.

Los colores predominantes en la misma son un naranja (#fa9606) y un rosa (#f04ff0), elegidos por su sinergia. Con estos colores en mente el título del juego quedó de la siguiente manera:

![Error en carga de Titulo del Juego](https://i.imgur.com/LmGQ4Aa.png)

El resto de elementos en la interfaz se pueden ver en el Apartado 3, relacionado con la misma. Aquí se pretendía explicar la lógica de la estética de esta y el por qué se ha elegido como tal.

##### 4.1.2 Modelos 3D

En cuanto a los Modelos 3D se refieren, son originales y creados en el programa gratuito _Blender_. Las texturas han sido desarrolladas directamente en Unity, debido al uso tanto de materiales básicos como emisivos.

Para hacer las piezas se han usado como base cubos con dos materiales, el base y el emisivo, de modo que de la impresión de que las aristas están rodeadas de neones iluminando la escena. Con este cubo se han podido crear estructuras similares a las piezas del videojuego _Tetris_, dándole más juego a las mecánicas del mismo. Juntando varias piezas quedaría así:

![Error en carga de Cubo](https://i.imgur.com/kZmvkW5.png)

En la imagen anterior se aprecia una bola blanca emisiva que hará las funciones del personaje. Será interactiva y responderá a las acciones del jugador.

Si nos centramos en los _Power Ups_ se han creado 3, uno para la _"Ceguera"_, otro para el _"Salto"_ y finalmente el de _"Ralentización"_. De modo que los modelos son así finalmente:

![Error en carga de Power Ups](https://i.imgur.com/aMgOd0p.png)


##### 4.1.3 Post Processing

Debido a la limitación que pone _Unity_ referente a los materiales emisivos, esto es, que solo se pueden aplicar a objetos estáticos, se ha decidido incluir el asset de _Post Processing_, que nos permite dotar de _bloom_ a los materiales emisivos.

### 4.2 Musica y efectos de sonido

##### 4.2.1 Musica
Las partituras de las melodías usadas en el menu y durante la partida son procedentes del usuario Dekkadeci de Musescore compartidas mediante la licencia Attribution 4.0 International (CC BY 4.0).

- **Partitura menú**: https://musescore.com/user/9996931/scores/2301611
- **Partitura juego**: https://musescore.com/user/9996931/scores/2297126
- **Usuario Dekkadeci en Musescore**: https://musescore.com/user/9996931
- **Licencia Creative Commons**: https://creativecommons.org/licenses/by/4.0/

Posteriormente, usando los midis de las partituras hemos aplicado el VSTi gratuito Digital Maths Chip Machine para darle un estilo retro a la música https://djavemcree.net/album/333119/digital-math-s-chiptune-machine-free-8bit-vst-synth
 ![DMCM](https://s3.amazonaws.com/content.sitezoogle.com/u/161458/a266ca226faef530cfa3330cf92bc88fe15b16a6/original/maxresdefault-14.jpg)
Hemos aplicado este VSTi a los midis de las partituras usando el DAW Reaper.
 
![Captura Reaper](https://i.imgur.com/zRQbGzO.jpg)
##### 4.2.2 Efectos sonido

Para la generacion de efectos de sonido hemos empleado el programa BFXR. Este programa es un generador de efectos de sonidos para juegos retro y es gratuito.  https://www.bfxr.net/

Se han incluido efectos de sonido para la muerte del jugador y para el cambio de eje.

![Captura Reaper](https://i.imgur.com/0CCJtUK.jpg)

Por otro lado se han incluido efectos de sonido en el power up de salto y de reducción de velocidad. Para ello hemos usado nuestra voz aplicando efectos de corrección de tono también en el DAW Reaper.
