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


#### Tarea: Implementar un zoom a la cámara del jugador que se controle con las teclas w-s.

Creamos un script CameraController. Creamos un puntero GameObject que asignaremos a la cámara. También añadimos los valores de zoom, para poder ajustarlos desde el editor.

```
public CinemachineVirtualCamera vcam;
public float nearZoom = 3.0f;
public float farZoom = 4.0f;
```

Las teclas w-s ya están en Unity como el eje vertical, que será > 0 con w, < 0 con s. Cuando detecte que pulsamos w acercará la cámara, y cuando detecte s la alejará.

```
void Update()
{
    float verticalInput = Input.GetAxis("Vertical");
    if(verticalInput > 0)
    {
        vcam.m_Lens.OrthographicSize = nearZoom;
    }
    if(verticalInput < 0)
    {
        vcam.m_Lens.OrthographicSize = farZoom;
    }

}
```

![](https://github.com/jsfabiani/Tarea_7_FDV/blob/main/gifs/FDV_7_gif_7.gif)


#### Tarea: Seleccionar un conjunto de teclas que permitan hacer el cambio entre dos cámaras . (Habilitar/Deshabilitar el gameobject de la cámara virtual)

En Update, añadimos este código, que comprueba si la cámara está habilitada para deshabilitarla y viceversa.

```
if(Input.GetKeyDown("c"))
{
    Debug.Log(vcam.enabled);
    if (vcam.enabled)
    {
        vcam.enabled = false;
    }
    else
    {
        vcam.enabled = true;
    }
}
```

![](https://github.com/jsfabiani/Tarea_7_FDV/blob/main/gifs/FDV_7_gif_8.gif)


#### Tarea: Crear un script para activar la cámara lenta cuando el personaje entre en colisión con un elemento de la escena que elijas para activar esta propiedad. 

#### Tarea: Crear un script para activar la cámara rápida cuando el personaje entre en colisión con un elemento de la escena que elijas para activar esta propiedad. 

La idea es que cuando colisionemos con algunos objetos, cambie la velocidad de ejecución durante un periodo que especifiquemos. Para eso, empezamos definiendo dos variables:


```
private float resetTime = 0.0f;
private float resetTimeCounter = 0.0f;
```

Las usaremos para establecer un temporizador para resetear la velocidad del juego cada vez que lo cambiemos. La primera será el tiempo que tenemos que esperar, la segunda un contador que actualizaremos en Update.

Para cambiar el tiempo de ejecución, definimos esta función:

```
void GameSpeed(float tscale, float delay)
{
    Time.timeScale = tscale;
    resetTime = Mathf.Abs(delay*tscale);
    resetTimeCounter = 0.0f;
}
```

Tiene dos variables: la primera es la nueva escala Time.timeScale del juego. La segunda es un delay en segundos. Asignamos a resetTime este delay multiplicado por la nueva timeScale, para que dure lo mismo en tiempo real aunque cambiemos la velocidad de ejecución del juego. Después ponemos el contador a 0.

En el Update controlamos el temporizador:

```
void Update()
{
    if (resetTime != 0.0f)
    {
        resetTimeCounter += Time.deltaTime;
        if (resetTimeCounter >= resetTime)
        {
            GameSpeed(1.0f, 0.0f);
        }
    }
...
}
```

Cuando activemos GameSpeed, resetTime será distinto de 0 y se entrará en el bucle. Entonces, aumentaremos resetTimeCounter cada frame. Cuando alcance el tiempo establecido, ejecutaremos GameSpeed sin delay para devolver la escala de tiempo. Vamos a poner que cuando choquemos contra un enemigo, se active la cámara lenta, y cuando cojamos una mejora se active la cámara rápida, ambos durante 2 segundos.

```
void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.tag == "Enemy")
    {
        GameSpeed(0.5f, 2.0f);          
    }

}

void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.tag == "PowerUp")
    {
        GameSpeed(2.0f, 2.0f);
    }
}
```

Tenemos al PowerUp definido como Trigger del ejercicio anterior, por eso usamos OnTriggerEnter2D en vez de OnCollisionEnter2D.

Aquí tenemos el resultado:

![](https://github.com/jsfabiani/Tarea_7_FDV/blob/main/gifs/FDV_7_gif_9.gif)
