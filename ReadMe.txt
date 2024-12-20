1. No olvide colocar las credenciales necesarias en el archivo appsettings.json


2. Para compilar el proyecto utilice el siguiente comando:

dotnet publish -c Release -o ./publish


3. en la carpeta compilada (publish) debe colocar los siguientes
   archivos para que el proyecto funcione correctamente:

- libwkhtmltox.dll
- libwkhtmltox.dylib
- libwkhtmltox.so