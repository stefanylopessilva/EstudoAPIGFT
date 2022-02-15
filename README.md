# API Cinema 🎥 
Aplicação desenvolvida para venda de ingressos de cinema, onde o usuário pode ver o catalogo de filmes cadastrados, com suas devidas informações, realizar o cadastro como usuário para logar e por fim realizar a reserva para o filme escolhido.
Para fins didáticos, assim que as migrations são realizadas, o banco é populado com alguns dados para serem testados.
Para ter acesso de administrador, e realizar atualizações, inclusões e deletar dados, entrar com os seguintes dados:
### Email: admin@dmin.com
### Senha: Admin2021
#
## Tecnologias 💻

- .NET 5.0
- ENTITY FRAMEWORK CORE 5.0.12
- MySql
- JWT
#
## Rodando o projeto 💻

Para explorar o projeto na sua máquina primeiro certifique-se que você tem o Git, .NET 5.0 e o ASP .NET CORE instalado.

1º- Com o git pré-instalado clone o projeto:

~~~shell
git clone 
~~~

2º - Navegue até o a pasta que você clonou no passo anterior e instale as dependencias do projeto, digitando:

~~~shell
dotnet restore
~~~

4º - Abra a pasta que você clonou no seu editor de preferência

5º - Com o MySql instalado, crie um banco de dados e renomeie a string de conexao JSON na raiz do projeto com o nome do banco e suas credenciais.

6º - Certifique se de que você tem o gerenciador de pacotes NuGet na sua máquina e instale o EF Core

7º Após isto rode as migrations para criar as tabelas no banco

~~~shell
dotnet ef database update
~~~

8º - Agora é só digitar o comando abaixo e depois digitar a url que irá aparecer no seu terminal na barra de pesquisa do navegador:
~~~shell
dotnet watch run
~~~
#
## Screenshots
#
## Realizando o registro de usuário
Assim que o registro é realizado, o usuário recebe a role "Usuario"

![image](https://user-images.githubusercontent.com/29932387/146578795-7ee84c0d-9be5-4964-9002-ea0daeb2ff35.png)
#
## Realizando o Login
![image](https://user-images.githubusercontent.com/29932387/146579281-772e78a2-e390-41c4-947f-c95b7fc10bdc.png)
#
## Autorização do Token
Com o login realizado, será gerado um token JWT para autenticação, clicando no campo Authorize, como a imagem abaixo, insira a palavra Bearer seguido do token previamente copiado.

![image](https://user-images.githubusercontent.com/29932387/146579786-a919dc78-9db5-45a4-9fac-47cf5cbe498d.png)

![image](https://user-images.githubusercontent.com/29932387/146579749-4063998c-dfad-452d-a041-6ab1f0e5b67e.png)
#
## Listagem de filmes
![image](https://user-images.githubusercontent.com/29932387/146588839-964e5a90-dc6a-4f4e-9b39-058182014b4c.png)

#
## Filme pelo Id
![image](https://user-images.githubusercontent.com/29932387/146588908-4897a6cc-d0ec-42e0-94f4-316a27dc94fc.png)

#
## Cadastrando novo Filme
Para cadastrar um novo filme, preencha através do formulário gerado os campos, sem preencher o Id(gerado automaticamente, porém lembre-se de desmarcar a opção para enviá-lo como nulo, caso execute o teste via Swagger), ImagemUrl(criada através da imagem selecionada), e Reserva.

![image](https://user-images.githubusercontent.com/29932387/146580158-00509967-4d50-48b6-bdef-520f0c20f359.png)
#
## Atualizando um filme
Informe o Id e os campos para serem alterados

![image](https://user-images.githubusercontent.com/29932387/146580942-4b6daa8b-bf03-41f2-bd47-e15dca222670.png)
#
## Apagando um filme
Informe o Id do filme que deseja apagar

![image](https://user-images.githubusercontent.com/29932387/146588494-23a7aa16-0385-4d00-9225-ade9cc5649eb.png)
#
O processo para lista de reservas, cadastro e exclusão de reservas se assemelha aos passos da imagem  acima para filmes.

