# 🤖 AI Chatbot — .NET Windows Forms

An AI-powered chatbot built with C# Windows Forms
integrated with Google Gemini API.

## Features
- Real-time AI responses via Gemini API
- Formatted responses with headings and bullets
- Error handling for API failures
- Enter key support for quick messaging
- Clear chat functionality

## Technologies Used
- C# / .NET Framework 4.7.2
- Windows Forms
- Google Gemini API (gemini-2.5-flash)
- Newtonsoft.Json 13.0.4
- Docker

## Setup Instructions
1. Clone this repository
2. Open `AIChatboat.sln` in Visual Studio
3. Install NuGet package: `Newtonsoft.Json`
4. Get free API key from https://aistudio.google.com/app/apikey
5. Open `Form1.cs` and replace `YOUR_GEMINI_API_KEY_HERE` with your key
6. Press F5 to run

## Docker
```bash
docker build -t ai-chatbot .
docker run ai-chatbot
```

## Reports
- Theory Report — see Theory_Report.pdf
- Practical Report — see Practical_Report.pdf
```

4. Click **"Commit changes"** green button

---

## STEP 7 — Verify Everything is Uploaded

Your final repository should look like this:
```
AI-Chatbot-DotNet/
  ├── Form1.cs
  ├── Form1.Designer.cs
  ├── Form1.resx
  ├── Program.cs
  ├── Dockerfile
  ├── App.config
  ├── AIChatboat.csproj
  ├── AIChatboat.sln
  ├── packages.config
  ├── Theory_Report.pdf
  ├── Practical_Report.pdf
  └── README.md
```

---
###Conclusion
This project successfully demonstrates the development of an AI-powered Chatbot using C#
 Windows Forms integrated with Google Gemini API. The application fulfills all requirements
 including a functional UI, AI integration, input validation, formatted responses, and
error handling. Docker containerization and GitHub submission requirements are also completed.
Overall this project provides a practical demonstration of how AI APIs can be integrated
 into real-world .NET desktop applications.
