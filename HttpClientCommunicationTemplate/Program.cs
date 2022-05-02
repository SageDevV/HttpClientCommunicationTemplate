using System.Net.Http.Json;
using System.Text.Json;

//Host e Endpoint juntas.
const string url = "";

//Instanciando um objeto que contém uma configuração de deserialização para validar a comparação no formato "CaseSensitive"
JsonSerializerOptions options = new JsonSerializerOptions() {  PropertyNameCaseInsensitive = true };

//Instanciando um Objeto do tipo HttpClient e determinando seu tempo de vida dentro de um using.
using(var clientGet = new HttpClient())
{
    //Verbo "Get" para leitura do arquivo Json
    var response = await clientGet.GetAsync(url);

    //Verificando se houve sucesso em se comunicar com a Api.
    if (response.IsSuccessStatusCode)
    {
        //Havendo sucesso, é feito a leitura do conteúdo do Json.
        var content = await response.Content.ReadAsStringAsync();

        //O Arquivo json é deserealizado e seu conteúdo é encapsulado em um objeto chamado "Employee".
        var employess = JsonSerializer.Deserialize<List<Employee>>(content);

        //Lendo uma coleção e gravando um log no console
        foreach (var employee in employess)
        {
            Console.WriteLine($"{employee.Id} {employee.Name} {employee.Age}");
        }
    }
    else
    {
        Console.WriteLine("Error");
        Console.ReadKey();
    }
}

//Instanciando um Objeto do tipo HttpClient e determinando seu tempo de vida dentro de um using.
using (var clientPost = new HttpClient())
{
    //Verbo "Post" para transferência de arquivo json para a Endpoint configurada.
    var response = await clientPost.PostAsJsonAsync(url, new Employee { Name = "Pedro", Age = 22});

    //Caso haver sucesso na transação, é disparado a mensagem confirmando.
    if (response.IsSuccessStatusCode)
        Console.WriteLine("Funcionário adcionado");
    else
        Console.WriteLine("Error");
}


//Objeto de destino.
class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
}