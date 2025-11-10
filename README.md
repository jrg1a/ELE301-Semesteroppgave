# 🔐 Adgangskontroll – Semesterprosjekt (Høst 2025)

Dette prosjektet er en del av semesteroppgaven i *Industriell IT* ved HVL.  
Oppgaven går ut på å utvikle et komplett system for adgangskontroll bestående av flere programmer som kommuniserer med hverandre basert på Windows Forms, database og SimSim-simulator.

---

## 🧩 Systemoversikt

Systemet består av **tre hovedkomponenter**:

1. **Sentral (Server)**  
   - Administrerer brukere og kortlesere  
   - Validerer kortID og PIN  
   - Logger alle adgangsforsøk (approved/denied)  
   - Håndterer og lagrer alarmer  
   - Genererer rapporter fra databasen  

2. **Kortleser (Client)**  
   - Representerer en fysisk kortleser ved en dør  
   - Sender forespørsler til Sentral via TCP  
   - Leser/sender signaler til **SimSim** (simulert sensor)  
   - Detekterer “dør brutt opp” og “dør åpen for lenge”  

3. **SimSim (Simulator)**  
   - Simulerer sensorer og aktuatorer  
   - Kommuniserer digitalt via porter  
   - Brukes til å teste uten fysisk maskinvare
  
---

## 🧠 Teknologi og språk

| Komponent | Teknologi |
|------------|------------|
| GUI | Windows Forms (.NET 8 / C#) |
| Nettverk | TCP-sockets (`TcpListener`, `TcpClient`) |
| Database | PostGres |
| Versjonskontroll | Git / GitHub |
| Dokumentasjon | Markdown, PDF-rapport |

