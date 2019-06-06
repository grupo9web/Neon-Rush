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


  
 
### 3.1 Diagrama de flujo



### 3.2 Menú de Inicio



### 3.3 Nivel



### 3.4 Menú de Opciones



### 3.5 Contáctanos



### 3.6 Pantalla de Juego



### 3.7 Game Over



## 4. Arte y Música

### Musica y efectos de sonido
#### Musica
Las partituras de las melodías usadas en el menu y durante la partida son procedentes del usuario Dekkadeci de Musescore compartidas mediante la licencia Attribution 4.0 International (CC BY 4.0).

- **Partitura menú**: https://musescore.com/user/9996931/scores/2301611
- **Partitura juego**: https://musescore.com/user/9996931/scores/2297126
- **Usuario Dekkadeci en Musescore**: https://musescore.com/user/9996931
- **Licencia Creative Commons**: https://creativecommons.org/licenses/by/4.0/

Posteriormente, usando los midis de las partituras hemos aplicado el VSTi gratuito Digital Maths Chip Machine para darle un estilo retro a la música https://djavemcree.net/album/333119/digital-math-s-chiptune-machine-free-8bit-vst-synth
 ![DMCM](https://s3.amazonaws.com/content.sitezoogle.com/u/161458/a266ca226faef530cfa3330cf92bc88fe15b16a6/original/maxresdefault-14.jpg)
Hemos aplicado este VSTi a los midis de las partituras usando el DAW Reaper.
 
![Captura Reaper](https://i.imgur.com/zRQbGzO.jpg)
#### Efectos sonido

Para la generacion de efectos de sonido hemos empleado el programa BFXR. Este programa es un generador de efectos de sonidos para juegos retro y es gratuito.  https://www.bfxr.net/

Se han incluido efectos de sonido para la muerte del jugador y para el cambio de eje.

![Captura Reaper](https://i.imgur.com/0CCJtUK.jpg)

Por otro lado se han incluido efectos de sonido en el power up de salto y de reducción de velocidad. Para ello hemos usado nuestra voz aplicando efectos de corrección de tono también en el DAW Reaper.
