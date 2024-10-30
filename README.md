# Tarea_7_FDV

### Tarea: Instalar el paquete CineMachine y configurar 2 cámaras virtuales con diferentes zonas de seguimiento al jugador. Mostar el efecto mediante un gif animado. 

Tenemos dos cámaras virtuales. La primera la hemos dejado con las opciones por defecto. A la segunda le hemos añadido Dead Zone y hemos aumentado el Soft Zone.

La primera cámara:

![](https://github.com/jsfabiani/Tarea_7_FDV/blob/main/screenshots/FDV_7_screenshot1.png)

![](https://github.com/jsfabiani/Tarea_7_FDV/blob/main/gifs/FDV_7_gif_1.gif)

La segunda cámara

![](https://github.com/jsfabiani/Tarea_7_FDV/blob/main/screenshots/FDV_7_screenshot_2.png)

![](https://github.com/jsfabiani/Tarea_7_FDV/blob/main/gifs/FDV_7_gif_2.gif)


### Tarea: Define un área de confinamiento diferente para cada una de las dos cámaras de la tarea anterior. Realiza una prueba de ejecución con el correspondiente gif animado que permita ver las diferencias.

Configuramos dos GameObjects vacíos para que sirvan de límites.

![](https://github.com/jsfabiani/Tarea_7_FDV/blob/main/screenshots/FDV_7_screenshot_3.png)

Cada cámara ocupa una mitad del mapa.

![](https://github.com/jsfabiani/Tarea_7_FDV/blob/main/gifs/FDV_7_gif_3.gif)

![](https://github.com/jsfabiani/Tarea_7_FDV/blob/main/gifs/FDV_7_gif_4.gif)


### Tarea: Agrega varios sprites en la escena que estén realizando un movimiento (mínimo 3). Genera una cámara adicional que le haga el seguimiento a dichos objetos.

Creamos tres sprites con un script para moverse entre dos puntos. Los añadimos al Target Group de la cámara.

![](https://github.com/jsfabiani/Tarea_7_FDV/blob/main/screenshots/FDV_7_screenshot_4.png)

![](https://github.com/jsfabiani/Tarea_7_FDV/blob/main/gifs/FDV_7_gif_5.gif)


### Tarea: Agrega 2 sprites adicionales en la escena que estén realizando un movimiento Genera una cámara adicional que le haga el seguimiento a dichos objetos, cada uno con un peso en la importancia del seguimiento diferente.

Añadimos otros dos sprites de drones al segundo Target Group. El de la izquierda tiene el doble de Weight, el de la derecha ni siquiera se ve en la cámara.

![](https://github.com/jsfabiani/Tarea_7_FDV/blob/main/screenshots/FDV_7_screenshot_5.png)

![](https://github.com/jsfabiani/Tarea_7_FDV/blob/main/gifs/FDV_7_gif_6.gif)
