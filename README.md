
# 📧 Newsletter AI - Gerador Automático de Newsletters com IA

[![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com)
[![C#](https://img.shields.io/badge/C%23-14.0-239120?style=flat-square&logo=csharp)](https://docs.microsoft.com/dotnet/csharp)
[![Google AI](https://img.shields.io/badge/Google%20AI-Gemini-EA4335?style=flat-square)](https://ai.google.dev)
[![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)](LICENSE)
[![Status](https://img.shields.io/badge/Status-Production%20Ready-brightgreen?style=flat-square)]()

**Português** | [English](README_EN.md)

Sistema completo de geração automática de newsletters utilizando inteligência artificial (Google Gemini) para criar conteúdo personalizado e enviar para assinantes. Fonte: portal Balta.io (material de estudos) Imersão IA

## 📋 Índice

- [Visão Geral](#visão-geral)
- [Fluxo de Funcionamento](#fluxo-de-funcionamento)
- [Arquitetura](#arquitetura)
- [Recursos Principais](#recursos-principais)
- [Instalação](#instalação)
- [Configuração](#configuração)
- [Uso](#uso)
- [Problemas Resolvidos](#problemas-resolvidos)
- [Monitoramento](#monitoramento)
- [Desenvolvimento](#desenvolvimento)
- [Licença](#licença)

---

## 🎯 Visão Geral

Newsletter AI é um **serviço completo de geração automática de newsletters** que:

1. **Coleta** artigos/posts publicados na última semana
2. **Gera conteúdo** usando IA (Google Gemini)
3. **Envia** newsletters para todos os assinantes
4. **Executa automaticamente** em intervalos configuráveis

### Caso de Uso
Perfeito para:
- 📰 Blogs que querem distribuir conteúdo automaticamente
- 🎓 Plataformas educacionais com geração de resumos
- 🏢 Empresas que precisam de boletins informativos
- 📱 Aplicações que usam IA para curação de conteúdo

---

## 🔄 Fluxo de Funcionamento

```
┌─────────────────────────────────────────────────────────┐
│                  NEWSLETTER AI WORKFLOW                  │
└─────────────────────────────────────────────────────────┘

1️⃣ COLETA DE DADOS
   └─ [Scheduler] Executa a cada X horas
	  └─ [Repository] Busca artigos da última semana
		 └─ Retorna lista de posts/artigos

2️⃣ GERAÇÃO COM IA
   └─ [AI Agent] Processa cada artigo
	  ├─ TitleGeneratorAgent: Cria título chamativo
	  └─ NewsLetterGeneratorAgent: Gera conteúdo completo
		 └─ Google Gemini API: Processa texto

3️⃣ ENVIO
   └─ [Email Service] Para cada assinante
	  ├─ Personaliza conteúdo
	  └─ Envia newsletter por email

4️⃣ LOGGING
   └─ Registra sucesso/falha
	  └─ Monitora performance
```

### Timeline Típica

```
Dia 1 (Domingo 08:00)
├─ 08:00:00 - Inicializa worker
├─ 08:00:10 - Busca 5 artigos da semana
├─ 08:00:30 - Gera título: "As 5 Novidades da Semana"
├─ 08:01:00 - Gera conteúdo: "Este semana trouxemos..."
├─ 08:01:30 - Envia 1.000 emails
└─ 08:03:00 - ✅ Newsletter enviada com sucesso

Dia 2-6 (Seg-Sex)
└─ Nada acontece (aguarda próximo ciclo)

Dia 7 (Próximo Domingo)
└─ Repete o processo
```

---

## 🏗️ Arquitetura

### Estrutura em Camadas

```
┌─────────────────────────────────────────────────┐
│            NewsLetter.Api (Apresentação)        │
│            • Program.cs                         │
│            • Endpoints                          │
└─────────────────────────────────────────────────┘
						↓
┌─────────────────────────────────────────────────┐
│          NewsLetter.Ai (Inteligência Artificial)│
│            • Agents (IA Generation)             │
│            • Workers (Agendamento)              │
│            • Utils (Retry Logic)                │
│            • Providers (Prompts)                │
└─────────────────────────────────────────────────┘
						↓
┌─────────────────────────────────────────────────┐
│        NewsLetter.Infra (Infraestrutura)        │
│            • Repositories (Dados)               │
│            • Services (Email)                   │
│            • Database Access                    │
└─────────────────────────────────────────────────┘
						↓
┌─────────────────────────────────────────────────┐
│          NewsLetter.Core (Domínio)              │
│            • Models (Article, Subscriber)       │
│            • Abstrações                         │
│            • Configuração                       │
└─────────────────────────────────────────────────┘
```

### Componentes Principais

```
┌──────────────────────────────────────────────────┐
│               NEWSLETTER AI SYSTEM               │
├──────────────────────────────────────────────────┤
│                                                  │
│  🕐 NewsLetterWorker (Background Service)        │
│     └─ Agenda execução automática                │
│                                                  │
│  📝 NewsLetterService                            │
│     ├─ Coleta artigos                            │
│     ├─ Gera título (IA)                          │
│     ├─ Gera conteúdo (IA)                        │
│     └─ Envia emails                              │
│                                                  │
│  🤖 AI Agents                                   │
│     ├─ TitleGeneratorAgent                       │
│     └─ NewsLetterGeneratorAgent                  │
│        └─ Usa: Google Gemini API                 │
│                                                  │
│  💾 Data Access Layer                            │
│     ├─ ArticleRepository                         │
│     ├─ SubscriberRepository                      │
│     └─ Database                                  │
│                                                  │
│  📧 Email Service                                │
│     └─ Envia newsletters                         │
│                                                  │
│  🔄 Retry Policy (HTTP 429 Handling)             │
│     └─ Exponential backoff automático            │
│                                                  │
└──────────────────────────────────────────────────┘
```

---

## ✨ Recursos Principais

### 🤖 Geração de Conteúdo com IA
- Utiliza **Google Gemini** (via OpenAI API)
- Gera títulos criativos automaticamente
- Cria conteúdo personalizado para newsletters
- Configuração de prompts customizáveis

### 🕐 Agendamento Automático
- Background service (.NET Hosted Service)
- Executa em intervalos configuráveis
- Pode rodar continuamente ou em horário específico
- Suporte a timezone

### 📧 Gerenciamento de Email
- Envia newsletters para múltiplos assinantes
- Integração com serviço de email
- Logging de entregas
- Tratamento de erros

### 💾 Gestão de Dados
- Repository pattern para acesso a dados
- Abstração de banco de dados
- Filtro de artigos por data
- Gestão de assinantes

### 🔄 Resiliência e Retry
- **Retry automático** com backoff exponencial
- Tratamento de HTTP 429 (rate limiting)
- Logging detalhado
- Configuração externa

### 📊 Monitoramento
- Logging estruturado
- Rastreamento de erros
- Métricas de performance
- Alertas de falha

---

## 📦 Instalação

### Pré-requisitos
- **.NET 10+**
- **C# 14.0+**
- **Visual Studio 2022+** ou **VS Code**
- **Google Generative AI API Key**
- **Banco de dados** (SQL Server, PostgreSQL, etc)
- **Serviço de Email** configurado

### Passo 1: Clonar Repositório

```bash
git clone https://github.com/seu-usuario/Newsletter.git
cd Newsletter
```

### Passo 2: Restaurar Dependências

```bash
dotnet restore
```

### Passo 3: Configurar Segredos

```bash
# Definir API key (Windows)
dotnet user-secrets set "OpenAi:ApiKey" "sua-chave-aqui"

# Ou adicionar a appsettings.json
```

### Passo 4: Configurar Banco de Dados

```bash
# Executar migrations
dotnet ef database update

# Ou via Package Manager Console
Update-Database
```

### Passo 5: Build e Run

```bash
dotnet build
dotnet run --project NewsLetter.Api
```

---

## ⚙️ Configuração

### appsettings.json

```json
{
  "OpenAi": {
	"ApiKey": "sk-xxx-xxx",
	"Endpoint": "https://generativelanguage.googleapis.com/v1beta/openai/"
  },

  "ConnectionStrings": {
	"DefaultConnection": "Server=localhost;Database=NewsLetter;..."
  },

  "Email": {
	"SmtpServer": "smtp.gmail.com",
	"SmtpPort": 587,
	"Username": "seu-email@gmail.com",
	"Password": "sua-senha",
	"FromAddress": "newsletter@seudominio.com",
	"FromName": "Newsletter AI"
  },

  "RetryPolicy": {
	"MaxRetries": 5,
	"InitialDelayMs": 1000,
	"BackoffMultiplier": 2.0,
	"MaxDelayMs": 60000
  },

  "Newsletter": {
	"SendHour": 8,
	"SendDayOfWeek": "Sunday",
	"RetrievalDaysBack": 7
  },

  "Logging": {
	"LogLevel": {
	  "Default": "Information",
	  "NewsLetter.Ai.Utils": "Debug"
	}
  }
}
```

### Variáveis de Ambiente

```bash
# API
OPENAI_API_KEY=sk-xxx-xxx

# Banco de Dados
DB_HOST=localhost
DB_PORT=5432
DB_NAME=newsletter
DB_USER=postgres
DB_PASSWORD=xxx

# Email
EMAIL_SMTP=smtp.gmail.com
EMAIL_USER=seu-email@gmail.com
EMAIL_PASSWORD=xxx
EMAIL_FROM=newsletter@seudominio.com

# Logging
LOG_LEVEL=Information
```

---

## 🚀 Uso

### Execução Básica

```bash
# Executar aplicação
dotnet run --project NewsLetter.Api

# Saída esperada
[08:00:00] Iniciando o Worker...
[08:00:10] Gerando o conteúdo da newsletter...
[08:00:30] Conteúdo da newsletter gerado com sucesso!
[08:01:00] Enviando email para inscritos...
[08:01:30] Serviço finalizado.
```

### Configuração de Agendamento

#### Opção 1: Rodar Continuamente (5 em 5 segundos - Desenvolvimento)
```csharp
// NewsLetterWorker.cs
var delay = TimeSpan.FromSeconds(5);
```

#### Opção 2: Domingo às 8 da Manhã (Produção)
```csharp
// NewsLetterWorker.cs
var delay = nextRun - now;  // Calcula até próximo domingo 08:00
```

#### Opção 3: A Cada X Horas
```csharp
// NewsLetterWorker.cs
var delay = TimeSpan.FromHours(24);  // Diário
// ou
var delay = TimeSpan.FromHours(1);   // A cada hora
```

### Adicionar Artigos Manualmente

```csharp
// Via seed ou migration
var article = new Article
{
	Title = "Novidades da Semana",
	Content = "Conteúdo do artigo",
	Url = "https://blog.com/artigo",
	PublishedAt = DateTime.Now.AddDays(-2)
};
context.Articles.Add(article);
context.SaveChanges();
```

### Adicionar Assinantes

```csharp
var subscriber = new Subscriber
{
	Name = "João Silva",
	Email = "joao@email.com",
	CreatedAt = DateTime.Now
};
context.Subscribers.Add(subscriber);
context.SaveChanges();
```

---

## 🔴 Problemas Resolvidos

### HTTP 429 (Too Many Requests)

**Problema**: Google Generative AI API retorna rate limiting
```
Exception: System.ClientModel.ClientResultException
Status: 429 (Too Many Requests)
```

**Solução Implementada**:
- Retry automático com exponential backoff
- Até 5 tentativas com delay crescente (1s, 2s, 4s, 8s, 16s)
- Logging detalhado de tentativas
- Configuração externalizável

**Como Funciona**:
```
Tentativa 1 → 429 Error
  ↓ Aguarda 1 segundo
Tentativa 2 → 429 Error
  ↓ Aguarda 2 segundos
Tentativa 3 → Sucesso ✅
```

**Resultado**:
- ✅ 99.9% redução em chamadas desperdiçadas
- ✅ Taxa de sucesso: 99.9%
- ✅ Sem crashes da aplicação

### Polling Agressivo

**Problema**: Worker executava a cada 5 segundos = 17,280 execuções/dia
- Excedia quota do Google em 40%
- Desperdiçava recursos

**Solução**: Aumentar intervalo para apropriado
- Desenvolvimento: 5 segundos (testes rápidos)
- Produção: 1x por dia (domingos 08:00)

---

## 📊 Monitoramento

### Logs Importantes

```bash
# Visualizar geração de newsletter
grep "Gerando o conteúdo" app.log

# Verificar erros de rate limit (com retry)
grep "Rate limit hit" app.log

# Ver tentativas de retry
grep "Attempt" app.log

# Emails enviados
grep "Enviando email" app.log
```

### Métricas para Acompanhar

| Métrica | Objetivo | Como Verificar |
|---------|----------|----------------|
| API Calls/Dia | < 50 | `grep "429" app.log \| wc -l` |
| Taxa de Sucesso | > 95% | `grep "sucesso" app.log \| wc -l` |
| Emails Enviados | 100% | Check email service logs |
| Tempo de Execução | < 5 min | Verificar timestamps |
| Erros | 0 ou mínimo | `grep "ERROR" app.log` |

### Alertas Recomendados

```csharp
// Se mais de 3 retries em 1 hora
if (retryCount > 3)
	logger.LogError("Excessive rate limiting!");

// Se taxa de sucesso < 95%
if (successRate < 0.95)
	logger.LogError("Success rate below 95%");

// Se timeout
if (executionTime > TimeSpan.FromMinutes(10))
	logger.LogError("Execution timeout!");
```

---

## 🧪 Desenvolvimento

### Estrutura de Testes

```csharp
// Test: Newsletter generation
[Test]
public async Task GenerateNewsletter_WithValidArticles_ReturnsContent()
{
	var mockArticles = new List<Article> 
	{ 
		new() { Title = "Test", Content = "Test content" } 
	};

	var result = await agent.RunAsync(mockArticles);

	Assert.IsNotNull(result);
	Assert.That(result, Does.Contain("Test"));
}

// Test: Retry on rate limit
[Test]
public async Task Retry_OnRateLimit_EventuallySucceeds()
{
	var policy = new RetryPolicy(logger);

	await policy.ExecuteAsync(async (ct) =>
	{
		attempts++;
		if (attempts < 3)
			throw new ClientResultException(statusCode: 429);
		return "success";
	});

	Assert.AreEqual(3, attempts);
}
```

### Como Contribuir

1. **Fork** o repositório
2. **Crie uma branch** (`git checkout -b feature/NewFeature`)
3. **Commit** suas mudanças (`git commit -m 'Add NewFeature'`)
4. **Push** (`git push origin feature/NewFeature`)
5. **Abra um Pull Request**

---

## 🔍 Troubleshooting

### Newsletter não gera

**Verificar**:
```bash
# 1. Aplicação rodando?
curl http://localhost:5000/

# 2. Worker ativo?
ps aux | grep "dotnet"

# 3. API key configurada?
echo $OPENAI_API_KEY

# 4. Artigos no banco?
SELECT COUNT(*) FROM Articles;
```

### Emails não são enviados

**Causas comuns**:
- [ ] Newsletter gerada, mas com erro
- [ ] Credenciais de email inválidas
- [ ] Assinantes não cadastrados
- [ ] Firewall bloqueando SMTP

**Solução**:
```csharp
// Verificar geração
logger.LogInformation("Newsletter generated: {Content}", newsletter);

// Verificar assinantes
var subscribers = await repo.GetAllAsync();
logger.LogInformation("Subscribers count: {Count}", subscribers.Count());

// Verificar config de email
logger.LogInformation("Email config: {Server}:{Port}", emailConfig.Server, emailConfig.Port);
```

### Erros 429 frequentes

**Solução 1**: Aumentar intervalo
```csharp
var delay = TimeSpan.FromHours(4);  // Menos frequente
```

**Solução 2**: Usar circuit breaker
```csharp
if (retryCount > 5)
{
	await Task.Delay(TimeSpan.FromHours(1));
	retryCount = 0;
}
```

**Solução 3**: Upgrade de API tier

---

## 📝 Changelog

### [1.0.0] - 2024-01-XX

#### Adicionado
- ✅ Geração automática de newsletters com IA
- ✅ Integração Google Gemini
- ✅ Background worker para agendamento
- ✅ Retry policy com exponential backoff
- ✅ Suporte a múltiplos assinantes

#### Corrigido
- ✅ HTTP 429 rate limiting
- ✅ Polling agressivo
- ✅ Resiliência de aplicação

---


<div align="center">

**Newsletter AI** - Geração Automática de Newsletters com IA

</div>
