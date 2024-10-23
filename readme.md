# Azure AISqlSense (Text-to-SQL) Service

### What is it?

An agent for generating Sql Queries from Natural Language. You configure that service to go against your database. You expose a chat app to your users. They make a request to see specific data. The service (1) transforms that request to a SQL Query and (2) executes it against the database. 

We use the SqlAgent feature from the Langchain framework which is considered one of the best agents for these operations. 

### Key Features

- Powered by LangChain: Built on top of the open-source LangChain framework, specifically using the SQL Agent feature.
- Azure Integration: Requires Azure OpenAI and Azure SQL Database for seamless integration with your data infrastructure.

    > [!NOTE]
    > The service consumes Azure Open AI and Azure SQL Database, but could easily be adapted for other environments.

- Development Flexibility: Compatible with both VS Code and PyCharm as development environments.

### Getting Started

1. Start: Familarize yourself stepping each component of the solution in a  Jupyter Notebook:
   - Navigate to the `notebook folder`.
   - Run the code using [Jupyter Notebook](https://code.visualstudio.com/docs/datascience/jupyter-notebooks) in either VS Code or PyCharm. 

      > [NOTE]
      > If you're not using Juypter Notebooks in Python and C# to build out your AI processes, you need to be. They're powerful and extremely simplte to use. 

2. Explore the Source Code:
   - Once comfortable, navigate to the `src folder` for a full version of the source code, which can be run as an API.
3. Explore Source Code for connecting to Microsoft Fabric:
   - For extra credit, navigate to the `Fabric folder` for source code that will go against Microsoft Fabric as a data source.

### Running the Source Code

### Contents

- `main.py`: Creates a FastAPI API to handle incoming requests and defines an endpoint for generating SQL queries based on user prompts.

- `orchestration_service.py`: Orchestrates the flow of data and control between different components and services in the application.

- `sql_agent\sql_agent_service.py`: Converts natural language prompts into SQL queries, handles the processing and execution of these queries, and returns the results to the user.

- `sql_agent\prompts.py`: Contains system prompts that define persona and directives on how to process user inputs, guiding the application in generating appropriate responses or actions.

- `sql_agent\credentials.env`: Stores secrets and sensitive information required for the service.

### How Do I Run It?

To run the engine locally as an API:

1. Install Dependencies:
        - Install the necessary Python FastAPI packages:
        ```bash
        pip install fastapi uvicorn
        ```
2. Navigate to the `src` folder:
    - Change directory to the `src` folder.

3. Start the FastAPI Server:
    - From the command line, run the following command:
        ```bash
        uvicorn main:app --reload
        ```
   - You should see the following in your terminal: 
        ```plantext
        **INFO**: Started server process.
        **INFO**: Waiting for application startup.
        **INFO**: Application startup complete.
        ```
    > [!NOTE]
    > At this point, the Fast API Server is running.

4. Test the API:
   - Open to your favorite `API Client Tool` (PostMan, Insomnia, Thunderclient) and make an HTTP POST request to:
        ```json
        Service URI: http://127.0.0.1:8000/generate-sql/
        Service HTTP Verb: POST
        HTTP Body:  {
	        "prompt": "what is the most popular menu item?"
        }
        ```

   - When complete, you should receive an HTTP status code of 200 and a JSON object that deserializes into the following model class:

        ```charp
        public class SqlResponseModel
        {
            public string Prompt { get; set; }
            public string FinalAnswer { get; set; }
            public string SqlStatement { get; set; }
            public string PromptTokens { get; set; }
            public string CompletionTokens { get; set; }
            public string TotalTokens { get; set; }
            public string TotalCost { get; set; }
            public string Explanation { get; set; }
            public string PromptTokensInt { get; set; }
            public string CompletionTokensInt { get; set; }
            public string TotalCostFloat { get; set; }
        }
        ```

    

### Secrts and Connection Information

In the `credentials.env`, file you'll need to define secrets and configuration for both Azure OpenAI and that for Azure SqlDatabase:

# Azure OpenAI Secrets
```plaintext
AZURE_OPENAI_ENDPOINT="< URL for Azure OpenAI service>/"
AZURE_OPENAI_API_KEY="< Azure OpenAI Key>"
GPT4_DEPLOYMENT_NAME="< Name of your Model Deployment >"
GPT35_DEPLOYMENT_NAME="< Name of your Model Deployment >"
```

# Azure SqlDatabase Secrets
```plaintext
SQL_SERVER_NAME="< xxxxxxxxxxx.database.windows.net >"  
SQL_SERVER_DATABASE="< Name of Sql Database >"
SQL_SERVER_USERNAME="< UserName >"
SQL_SERVER_PASSWORD="< Password >"
```


### What's Coming: 

1. Security, security, security (adding identity/security)
2. Python version that support classes.
3. A version implemented with LLama Index.


### Acknowledgments

Big thanks to `Pablo Marin` from Microsoft! We utilized parts of his work from the [GPT-Azure-Search-Engine](https://github.com/pablomarin/GPT-Azure-Search-Engine) repository. His repo offers a comprehensive set of examples for leveraging data and OpenAI—definitely worth checking out.

## License
MIT License
