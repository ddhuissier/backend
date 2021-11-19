using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService" à la fois dans le code et le fichier de configuration.
[ServiceContract]
public interface IService
{
	[OperationContract]
	[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate ="SayHello/")]
	string SayHello();

	[OperationContract]
	[WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetData/{value}/")]
	string GetData(string value);
}
