//
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace Silanis.ESL.API
{
	
	
	public class TextualSignatureStyle
	{
		
		// Fields
		
		// Accessors
		    
    [JsonProperty("color")]
    public String Color
    {
                get; set;
        }
    
		    
    [JsonProperty("font")]
    public String Font
    {
                get; set;
        }
    
		
	
	}
}