# ProyIngWeb_Vue
Nombre: Doménica Rueda

* [Login](#Login)

* [Admin](#Admin)
## :bust_in_silhouette:Login


![image](https://user-images.githubusercontent.com/62667937/195994935-52cac8e7-8c6b-4ba1-82fc-7379b367ef93.png)

![image](https://user-images.githubusercontent.com/62667937/195994957-63361793-1a92-4be0-aef8-06089e153e1d.png)

![image](https://user-images.githubusercontent.com/62667937/195994983-ebcf9d6a-cce5-4288-9b35-f082d155fb27.png)



1. Para la creación del login primero se creo un nuevo modelo desde nuestro backend 


![image](https://user-images.githubusercontent.com/62667937/195994360-a475c5ee-ea4c-442d-b2e6-22378e0094d7.png)
![image](https://user-images.githubusercontent.com/62667937/195994367-5a00ab0e-030f-43a2-b334-8aee007d6cfb.png)


2. Se crearon en el controlador los métodos necesarios para el Login, generar los tokens y la encriptacion de la contraseña 


![image](https://user-images.githubusercontent.com/62667937/195994434-ee1ad190-a762-446a-b217-6cdccd570f7e.png)
![image](https://user-images.githubusercontent.com/62667937/195994453-0eb718ca-d712-4854-b094-c79c64937e9b.png)


3. Se agregaron 2 variables en el archivo appsettings para la firma y acceso de los tokens 


![image](https://user-images.githubusercontent.com/62667937/195994509-03f79079-fdf1-4fea-a716-51b40700728d.png)


## :green_circle:Vue

1. Desde el front-end se agrego un nuevo componente  login el cual tendra el diseño HTML


![image](https://user-images.githubusercontent.com/62667937/195994637-31b5fb85-41f9-447d-8d42-d3e6633aecb0.png)
 1.1 Método para ingresar al sistema y validacion de errores
 
 
 ![image](https://user-images.githubusercontent.com/62667937/195994700-33c19c34-c628-4078-bf18-a7bb6e566340.png)

2. Agregar la ruta en el componente router 

![image](https://user-images.githubusercontent.com/62667937/195994720-f2b8fc2a-460a-46da-bee3-b80715eb0d02.png)


 2.1 Acceso a rutas dependiendo el rol si es que ha ingresado al sistema correctamente
 
 
 ![image](https://user-images.githubusercontent.com/62667937/195994752-2fd20f8a-e7c1-45a9-86b6-b9e6919510dd.png)

4. Agregar variables y metodos en el componente store


6.1 Metodos para guaradar el toke, verificar si el usaurio ingreso de forma correcta y salir (remover el token del localStorage)


![image](https://user-images.githubusercontent.com/62667937/195994839-4e54f097-95f3-4863-8b4d-fd525b2bd586.png)

## :closed_lock_with_key:Admin

El admin del proyecto es capaz de alimentar 3 tablas:
- `Medicamentos`
- `Sintomas`
- `Relacion medicamento-sintoma`

La relación de estas tablas en la base de datos es la siguiente:


![image](https://user-images.githubusercontent.com/62667937/202650538-a885adf0-8c56-4752-a5fd-af6cb33797af.png)


En el backend del proyecto cada tabla tiene 

- `Capa de datos` : Una clase para mapeamiento de datos y DbContext

![image](https://user-images.githubusercontent.com/62667937/202651445-52ab00bb-0810-4814-b7ec-5c91a4b6fd4d.png)

- `Capa Entidad` : Una clase para cada tabla 

![image](https://user-images.githubusercontent.com/62667937/202651589-f8c121d3-a069-4bda-b6a3-d59443aa4472.png)

- `Modelos` : Modelos para cada tabla 

![image](https://user-images.githubusercontent.com/62667937/202651742-4566668d-2989-447f-9621-ca1bbbebc829.png)

- `Controladores` : Poseen los métodos GET, POST, DELETE, PUT  de cada tabla 

![image](https://user-images.githubusercontent.com/62667937/202652334-04ed9f8d-4294-46b3-9858-6f5beb5b2502.png)











