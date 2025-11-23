# Gadget-Hub – Service-Oriented Ordering System

Gadget-Hub is a **Service-Oriented Architecture (SOA)**–based ordering platform that fetches and compares real-time product quotations from **three electronic distributors**:

- 🏢 TechWorld  
- 🏢 ElectroCom  
- 🏢 Gadget Central  

The system ensures customers receive the **best price instantly** by aggregating quotations through independent microservices and returning a unified comparison.

---

## 🚀 Key Features

- 🔌 **SOA Architecture** – Each distributor runs as an independent service  
- 📡 **API Gateway** – Central point for receiving and dispatching client requests  
- ⚙️ **Real-Time Quotation Comparison**  
- 📈 **Price Ranking System** – Automatically highlights the best offer  
- 🌐 **Modern React Frontend** – Clean UI for entering product queries  
- 🔒 **Secure Communication** between services  
- 🗄️ **SQL Server Database** for storing product & request logs  

---

## 🛠️ Tech Stack

### **Frontend**
- React.js  
- Axios  
- Tailwind / CSS  

### **Backend**
- ASP.NET Core Web API  
- C#  
- RESTful Services  

### **Database**
- Microsoft SQL Server  

### **Architecture**
- Service-Oriented Architecture  
- API Gateway Pattern  
- Distributed Microservices

---

## 🧪 How to Run
- Backend
```
cd GatewayAPI
dotnet run
```
- Start each distributor service:
```
cd Distributor1Service
dotnet run
```
- Frontend
```
cd frontend
npm install
npm run dev
```

---

## 👨‍💻 Author

- Chiran Keshawa Weerasekara
- Software Engineering Student | Full-Stack Developer
