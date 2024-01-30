using System.Security.Cryptography;
using UserService.Controllers;

namespace TestProject;

[TestFixture]
public class RSAToolsTests {
    [Test]
    public void TestDecryptMethod() {
        string path = "D:\\workspace\\gb.ru.messenger.applications\\UserService\\rsa\\private_key.pem";
        RSA plaintext = RSATools.GetPrivateKey(path);

        Assert.NotNull(plaintext);
    }
    
}