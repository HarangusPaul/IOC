using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TaskDisplay : MonoBehaviour
{
    // Reference to the task title map (this GameObject will be shown/hidden)
    public GameObject taskTitleMap;

    // Reference to the empty GameObject (this will have multiple child objects)
    public GameObject triggerObject;
    
    public GameObject damageObject;
    
    public GameObject question;
    public GameObject questionText;
    
    public TextMeshProUGUI taskText;
    
    public TextMeshProUGUI Money;
    
    public Button button1;
    public Button button2;

    // The text that you want to set when clicked
    public string clickedText = "Task Completed!";

    // The proximity threshold for when to show the taskTitleMap
    public float proximityThreshold = 2f;

    private Transform child;
    private Transform childDmg;

    // The data structure to hold the question and answers (you can replace this with a file loading mechanism later)
    private Question[] questions = {
        //Stiinte ale naturii
        //Foarte Usoare
        new Question("Ce este necesar pentru a susține viața?", new string[] { "Aer, apă, hrană", "Nisip, pietre, lumină" }, "a"),
        new Question("Ce este un organism viu?", new string[] { "Un obiect care crește și se înmulțește", "O piatră mare și grea" }, "a"),
        new Question("Ce fac plantele cu dioxidul de carbon?", new string[] { "Îl elimină în aer", "Îl folosesc pentru fotosinteză" }, "b"),
        new Question("Care este rolul principal al rădăcinilor plantelor?", new string[] { "Absorb apă și substanțe nutritive", "Produc oxigen" }, "a"),
        new Question("Ce tip de animal este găina?", new string[] { "Mamifer", "Pasăre" }, "b"),
        new Question("Din ce sunt alcătuiți solzii peștilor?", new string[] { "Oase", "Material dur, protector" }, "b"),
        new Question("Ce fenomen are loc atunci  când o plantă se orientează spre lumină?", new string[] {"Fotosinteză", "Fototropism" }, "b"),
        new Question("Care este mediul principal de viață al amfibienilor?", new string[] {"Pe uscat","În apă și pe uscat"}, "b"),
        new Question("Cum se numesc păsările care călătoresc pe distanțe mari?", new string[] {"Migratoare","Sedentare"}, "a"),
        new Question("Ce organ ajută peștii să respire?", new string[] {"Branhii","Plămâni"}, "a"),
        new Question("Ce proces ajută plantele să producă oxigen?", new string[] {"Respirația","Fotosinteza"}, "b"),
        new Question("Cum se deplsează șerpii?", new string[] {"Prin târâre","Prin zbor"}, "a"),
        new Question("Care este mediul de viață al unui rechin?", new string[] {"Apa dulce","Apa sărată"}, "b"),
        new Question("Ce element este esențial pentru respirația viețuitoarelor?", new string[] {"Oxigen","Dioxid de carbon"}, "a"),
        new Question("Ce grupă de animale depune icre?", new string[] {"Mamifere","Pești"}, "b"),
        new Question("Ce părți ale corpului ajută păsările să zboare?", new string[] {"Aripile","Picioarele"}, "a"),
        new Question("Care viețuitoare elimină oxigen în aer?", new string[] {"Plantele","Animalele"}, "a"),
        new Question("Ce este germinația?", new string[] {"Procesul prin care o sămânță încolțește","Hrănirea plantelor cu apă"}, "a"),
        new Question("Cum se numesc animalele care se hrănesc cu plante și cu animale?", new string[] {"Carnivore","Omnivore"}, "b"),
        new Question("Ce element este necesar pentru fotosinteză?", new string[] {"Lumină","Oxigen"}, "a"),
        //Usoare
        new Question("Care este rolul principal al branhiei la pești?", new string[] { "Respirația", "Hrănirea" }, "a"),
        new Question("Ce este poluarea?", new string[] { "Modificarea naturală a mediului", "Introducerea de substanțe dăunătoare în mediu" }, "b"),
        new Question("Cum obțin plantele apa necesară?", new string[] { "Din aer", "Din sol" }, "b"),
        new Question("Care este principalul motiv al migrației păsărilor?", new string[] { "Schimbarea habitatului", "Lipsa hranei în sezonul rece" }, "b"),
        new Question("Ce tip de animal este crocodilul?", new string[] { "Mamifer", "Reptilă" }, "b"),
        new Question("Cum contribuie plantele la circuitul apei?", new string[] { "Absorb apa și o elimină prin transpirație", "Transformă apa în nutrienți" }, "a"),
        new Question("Care este diferența principală între păsări și mamifere?", new string[] { "Modul de deplasare", "Modul de reproducere" }, "b"),
        new Question("Cum își protejează camuflajul un animal?", new string[] { "Îl ajută să atragă prada", "Îl ascunde de prădători" }, "b"),
        new Question("Ce resursă naturală este considerată regenerabilă?", new string[] { "Petrolul", "Energia solară" }, "b"),
        new Question("Care viețuitoare se hrănesc cu insecte?", new string[] { "Reptilele", "Plantele carnivore" }, "b"),
        new Question("Ce proces are loc când apa trece din stare lichidă în stare gazoasă?", new string[] { "Solidificare", "Vaporizare" }, "b"),
        new Question("Ce tip de sol este cel mai fertil?", new string[] { "Nisipul", "Humusul" }, "b"),
        new Question("Cum ajută pădurile la menținerea calității aerului?", new string[] { "Produc oxigen și absorb dioxid de carbon", "Consumă oxigen pentru creștere" }, "a"),
        new Question("Ce organ de simț este cel mai dezvoltat la lilieci?", new string[] { "Văzul", "Auzul" }, "b"),
        new Question("Ce caracteristică ajută pinguinii să supraviețuiască în frig?", new string[] { "Strat gros de grăsime", "Culoarea penajului" }, "a"),
        new Question("Ce tip de mișcare este specifică stelelor de mare?", new string[] { "Târâre", "Locomoție cu ajutorul brațelor" }, "b"),
        new Question("Ce se întâmplă cu o plantă lipsită de lumină?", new string[] { "Se oprește fotosinteza", "Crește mai repede" }, "a"),
        new Question("Cum influențează poluarea solului agricultura?", new string[] { "Crește producția agricolă", "Reduce fertilitatea solului" }, "b"),
        new Question("Care este principala sursă de energie pentru fotosinteză?", new string[] { "Dioxidul de carbon", "Lumina solară" }, "b"),
        new Question("Cum respiră animalele acvatice?", new string[] { "Cu plămâni", "Cu branhii" }, "b"),
        // Medii:
        new Question("Ce proprietate au metalele care permite conducerea electricității?", new string[] {"Conductivitatea electrică", "Elasticitatea"}, "a"),
        new Question("Cum se numesc schimbările de la stare lichidă la stare solidă?", new string[] {"Vaporizare", "Solidificare"}, "b"),
        new Question("Care este diferența principală între animalele erbivore și carnivore?", new string[] {"Modul de reproducere", "Tipul de hrană"}, "b"),
        new Question("Ce efect are gravitația asupra obiectelor?", new string[] {"Le împinge în sus", "Le atrage spre centrul Pământului"}, "b"),
        new Question("Ce animal folosește ecolocația pentru a se orienta?", new string[] {"Delfinul", "Ursul"}, "a"),
        new Question("Cum contribuie rădăcinile plantelor la prevenirea eroziunii solului?", new string[] {"Fixează solul", "Îl fertilizează"}, "a"),
        new Question("Ce tip de interacțiune este frecarea?", new string[] {"Interacțiune de contact", "Interacțiune gravitațională"}, "a"),
        new Question("Cum se numesc resursele care nu se epuizează?", new string[] {"Regenerabile", "Neregenerabile"}, "a"),
        new Question("Ce organ folosește broasca pentru respirație pe uscat?", new string[] {"Branhii", "Plămâni"}, "b"),
        new Question("Ce fenomen produce vântul?", new string[] {"Mișcarea apei", "Mișcarea aerului"}, "b"),
        new Question("Ce plantă poate supraviețui în deșert?", new string[] {"Cactusul", "Nufărul"}, "a"),
        new Question("Cum se deplasează păianjenii?", new string[] {"Cu șase picioare", "Cu opt picioare"}, "b"),
        new Question("Care este diferența principală între stările de agregare solidă și lichidă?", new string[] {"Forma și volumul", "Masa"}, "a"),
        new Question("Ce viețuitoare trăiesc în simbioză cu florile, polenizându-le?", new string[] {"Albinele", "Lăcustele"}, "a"),
        new Question("Cum se numesc pădurile care rămân verzi tot anul?", new string[] {"Păduri de foioase", "Păduri de conifere"}, "b"),
        new Question("Ce organ ajută crocodilul să vadă sub apă?", new string[] {"Membrana oculară", "Branhiile"}, "a"),
        new Question("Cum influențează schimbările climatice circuitul apei?", new string[] {"Îl accelerează sau îl încetinesc", "Nu au efect asupra circuitului apei"}, "a"),
        new Question("Ce este reciclarea?", new string[] {"Arderea deșeurilor", "Reutilizarea materialelor pentru a produce altele noi"}, "b"),
        new Question("Cum își păstrează balenele temperatura corpului în apă rece?", new string[] {"Prin stratul gros de grăsime", "Prin înot constant"}, "a"),
        new Question("Ce este efectul de seră?", new string[] {"Încălzirea atmosferei datorită gazelor reținute", "Creșterea biodiversității"}, "a"),
        // Grele:
        new Question("Ce se întâmplă cu temperatura corpului animalelor hibernante?", new string[] {"Rămâne constantă", "Scade semnificativ"}, "b"),
        new Question("Care este cauza principală a extincției unor specii?", new string[] {"Lipsa apei", "Distrugerea habitatului"}, "b"),
        new Question("Cum funcționează adaptarea luminoasă la plante?", new string[] {"Produc oxigen doar noaptea", "Se orientează spre sursa de lumină"}, "b"),
        new Question("Cum influențează poluarea aerului sănătatea oamenilor?", new string[] {"Crește rata de boli respiratorii", "Nu are efect semnificativ"}, "a"),
        new Question("Cum determină temperaturile ridicate topirea ghețarilor?", new string[] {"Prin creșterea salinității apei", "Prin încălzirea globală"}, "b"),
        new Question("Care este rolul principal al stratului de ozon?", new string[] {"Protejează împotriva razelor ultraviolete", "Crește temperatura Pământului"}, "a"),
        new Question("Ce rol au ploile acide asupra solului?", new string[] {"Îl fertilizează", "Îi scad fertilitatea prin modificarea pH-ului"}, "b"),
        new Question("Cum poate fi redusă amprenta de carbon?", new string[] {"Prin utilizarea surselor de energie regenerabilă", "Prin consum sporit de resurse naturale"}, "a"),
        new Question("Cum afectează defrișările biodiversitatea?", new string[] {"O reduc drastic", "O cresc prin schimbarea habitatului"}, "a"),
        new Question("Cum detectează delfinii prada în ape întunecate?", new string[] {"Prin ecolocație", "Prin simțul tactil"}, "a"),
        new Question("Ce este rezistența la antibiotice?", new string[] {"Capacitatea bacteriilor de a supraviețui antibioticelor", "Eficiența crescută a medicamentelor"}, "a"),
        new Question("Cum influențează creșterea nivelului mării zonele de coastă?", new string[] {"Crește suprafața uscatului", "Provoacă eroziuni și inundații"}, "b"),
        new Question("Ce permite sângelui cald la mamifere?", new string[] {"Reglarea temperaturii interne", "Creșterea cantității de oxigen"}, "a"),
        new Question("Cum se deplasează plantele carnivore pentru a prinde prada?", new string[] {"Mișcare rapidă a frunzelor specializate", "Creștere spre pradă"}, "a"),
        new Question("Cum își produc plantele hrana?", new string[] {"Prin absorbția directă a oxigenului", "Prin fotosinteză"}, "b"),
        new Question("Ce determină formarea fulgerelor?", new string[] {"Contactul dintre curenții de aer cald și rece", "Combustia oxigenului"}, "a"),
        new Question("Care este rolul peștilor într-un ecosistem acvatic?", new string[] {"Consumatori primari", "Consumatori secundari și de top"}, "b"),
        new Question("Cum influențează urbanizarea fauna sălbatică?", new string[] {"O sprijină prin crearea de habitate noi", "O limitează prin distrugerea habitatelor naturale"}, "b"),
        new Question("Cum este afectată Terra de lipsa stratului de ozon?", new string[] {"Crește expunerea la radiații UV", "Scade temperatura globală"}, "a"),
        new Question("Ce mecanism folosește cameleonul pentru camuflaj?", new string[] {"Adaptarea temperaturii corpului", "Schimbarea culorii pielii"}, "b"),

        // Foarte grele:
        new Question("Cum influențează concentrația ridicată de dioxid de carbon procesul de fotosinteză?", new string[] {"Îl accelerează până la un anumit prag", "Îl oprește complet"}, "a"),
        new Question("Cum afectează acidificarea oceanelor ecosistemele marine?", new string[] {"Crește biodiversitatea", "Distruge coralii și afectează viețuitoarele cu schelet calcaros"}, "b"),
        new Question("Cum se poate demonstra că aerul exercită presiune?", new string[] {"Folosind un balon umflat sub apă", "Prin încălzirea unui obiect metalic"}, "a"),
        new Question("Ce efect are poluarea cu microplastice asupra peștilor?", new string[] {"Crește rata de reproducere", "Afectează sistemele lor digestive și respiratorii"}, "b"),
        new Question("Cum funcționează câmpul magnetic al Pământului?", new string[] {"Atrage obiecte către suprafață", "Ghidează busolele și protejează planeta de radiații solare"}, "b"),
        new Question("Cum determină schimbările climatice migrația păsărilor?", new string[] {"Migrarea mai devreme sau mai târziu decât de obicei", "Creșterea distanțelor de migrare"}, "a"),
        new Question("Ce este bioluminiscența?", new string[] {"Lumina produsă de organisme vii", "Reflexia luminii naturale pe suprafața apei"}, "a"),
        new Question("Cum influențează defrișările ploile dintr-o regiune?", new string[] {"Cresc cantitatea de precipitații", "Reduc frecvența și intensitatea ploilor"}, "b"),
        new Question("Cum își reglează pinguinii temperatura corporală în colonii?", new string[] {"Prin ventilația aripilor", "Prin apropierea unul de altul pentru a reține căldura"}, "b"),
        new Question("Ce determină formarea curenților oceanici?", new string[] {"Diferențele de temperatură și salinitate", "Evaporarea apei de la suprafață"}, "a"),
        new Question("Cum afectează poluarea cu metale grele lanțul trofic?", new string[] {"Este eliminată rapid din mediu", "Se acumulează în organismele superioare"}, "b"),
        new Question("Care este mecanismul principal de reproducere a coralilor?", new string[] {"Reproducere sexuală prin eliberarea gametelor în apă", "Diviziunea celulelor coralului adult"}, "a"),
        new Question("Cum se adaptează cactusul la medii aride?", new string[] {"Depozitează apă în frunze", "Depozitează apă în tulpină și are spini pentru a reduce evaporația"}, "b"),
        new Question("Ce rol au mangrovele în ecosistemele de coastă?", new string[] {"Protejează malurile de eroziune și oferă habitat pentru viețuitoare", "Îmbunătățesc doar calitatea apei"}, "a"),
        new Question("Cum influențează încălzirea globală circulația apei în atmosferă?", new string[] {"Crește rata de evaporare și precipitații extreme", "O stabilizează"}, "a"),
        new Question("Cum respiră păianjenii?", new string[] {"Prin plămâni și tuburi speciale numite trahei", "Prin branhii"}, "a"),
        new Question("Care este principalul pericol pentru animalele marine mari, precum balenele?", new string[] {"Prădătorii naturali", "Coliziunile cu nave și poluarea fonică"}, "b"),
        new Question("Ce determină schimbarea culorii la cameleoni?", new string[] {"Reacția la lumină și temperatură", "Modificări în structura celulelor pielii pentru comunicare și camuflaj"}, "b"),
        new Question("Ce este eutrofizarea?", new string[] {"Creșterea excesivă a algelor din cauza poluării cu nutrienți", "Lipsa oxigenului din apă"}, "a"),
        new Question("Cum protejează ecosistemele sănătoase împotriva dezastrelor naturale?", new string[] {"Prin absorbția impactului și stabilizarea mediului", "Prin crearea de bariere artificiale"}, "a"),


        //Istorie
        //Usoare
        new Question("Ce este istoria?", new string[] { "O poveste despre trecut", "O poveste despre viitor" }, "a"),
        new Question("Cum se numește steagul unei țări?", new string[] { "Imn", "Drapel" }, "b"),
        new Question("Cine a fost Decebal?", new string[] { "Un rege dac", "Un împărat roman" }, "a"),
        new Question("Ce popor a cucerit Dacia?", new string[] { "Grecii", "Romanii" }, "b"),
        new Question("Cum se numea capitala Daciei?", new string[] { "Sarmizegetusa", "Roma" }, "a"),
        new Question("Care este simbolul național al României care se cântă la sărbători?", new string[] { "Drapelul", "Imnul" }, "b"),
        new Question("Ce reprezintă culorile de pe drapelul României?", new string[] { "Curaj, justiție, libertate", "Pace, război, unitate" }, "a"),
        new Question("Cum se numește ziua națională a României?", new string[] { "1 Decembrie", "24 Ianuarie" }, "a"),
        new Question("Ce simbol al dacilor era în formă de animal?", new string[] { "Lupul dacic", "Vulturul dacic" }, "a"),
        new Question("Cine a fost Traian?", new string[] { "Un rege dac", "Un împărat roman" }, "b"),
        new Question("Ce este o cetate?", new string[] { "Un oraș fortificat", "Un câmp deschis" }, "a"),
        new Question("Cum se numește procesul prin care dacii au adoptat cultura romană?", new string[] { "Romanizare", "Globalizare" }, "a"),
        new Question("Cum se numea conducătorul Moldovei care a fost și sfânt?", new string[] { "Ștefan cel Mare", "Mihai Viteazul" }, "a"),
        new Question("Cum se numește sărbătoarea din 24 ianuarie?", new string[] { "Ziua Națională", "Unirea Principatelor Române" }, "b"),
        new Question("Ce înseamnă trecut?", new string[] { "Ce se va întâmpla în viitor", "Ce s-a întâmplat înainte" }, "b"),
        new Question("Care este imnul României?", new string[] { "Trei culori", "Deșteaptă-te, române!" }, "b"),
        new Question("Ce animal apare pe stema României?", new string[] { "Vulturul", "Lupul" }, "a"),
        new Question("Cum se numește cartea care povestește despre trecut?", new string[] { "Cronologie", "Manual" }, "a"),
        new Question("Ce material foloseau dacii pentru arme?", new string[] { "Lemnul", "Fierul" }, "b"),
        new Question("Cum se numea zeul principal al dacilor?", new string[] { "Zamolxis", "Jupiter" }, "a"),
        //Medii spre usoare
        new Question("Cine a fost Alexandru Ioan Cuza?", new string[] { "Domnitor al Moldovei și Țării Românești", "Rege al Daciei" }, "a"),
        new Question("Ce reprezintă Unirea din 1859?", new string[] { "Independența României", "Unirea Moldovei cu Țara Românească" }, "b"),
        new Question("Cum se numea stindardul dacilor?", new string[] { "Cap de dragon", "Cap de lup" }, "b"),
        new Question("Ce înfățișează Columna lui Traian?", new string[] { "Războaiele daco-romane", "Viața împăratului Traian" }, "a"),
        new Question("Cum se numea regele dac care a unit triburile?", new string[] { "Decebal", "Burebista" }, "b"),
        new Question("Ce simbolizează culorile drapelului României?", new string[] { "Roșu: sângele eroilor, galben: grânele, albastru: cerul", "Verde: natura, roșu: forța, albastru: pacea" }, "a"),
        new Question("Cine a fost Mihai Viteazul?", new string[] { "Un domnitor care a unit pentru prima dată Țările Române", "Un împărat roman" }, "a"),
        new Question("Ce oraș a fost construit de romani în locul capitalei dacice?", new string[] { "Alba Iulia", "Ulpia Traiana Sarmizegetusa" }, "b"),
        new Question("Ce popor a introdus limba latină în Dacia?", new string[] { "Grecii", "Romanii" }, "b"),
        new Question("Cum se numea împăratul care a început războaiele daco-romane?", new string[] { "Traian", "Nero" }, "a"),
        new Question("Ce simbol foloseau dacii pentru a-și reprezenta curajul?", new string[] { "Soarele", "Lupul dacic" }, "b"),
        new Question("Care era numele podului construit de romani peste Dunăre?", new string[] { "Podul lui Apolodor", "Podul lui Decebal" }, "a"),
        new Question("Ce înseamnă 'Dacia'?", new string[] { "Numele unei provincii romane din Italia", "Numele teritoriului locuit de daci" }, "b"),
        new Question("Cine a fost Mircea cel Bătrân?", new string[] { "Un domnitor al Țării Românești", "Un împărat roman" }, "a"),
        new Question("Cum se numește locul unde dacii se întâlneau pentru ceremonii religioase?", new string[] { "Forum", "Sanctuar" }, "b"),
        new Question("Ce obiect era folosit de romani pentru a comemora victoriile lor?", new string[] { "Stindardul", "Columna" }, "b"),
        new Question("Cine a fost Vlad Țepeș?", new string[] { "Un domnitor al Țării Românești cunoscut pentru lupta împotriva otomanilor", "Un rege dac" }, "a"),
        new Question("Cum erau organizate triburile dacice?", new string[] { "Sub conducerea unui rege", "Fără conducere centralizată" }, "a"),
        new Question("Ce eveniment a avut loc la 1600?", new string[] { "Mihai Viteazul a unit pentru prima dată Țările Române", "Dacia a fost cucerită de romani" }, "a"),
        new Question("Cum se numea moneda folosită de romani?", new string[] { "Dinar", "Denarius" }, "b"),
        //Medii
        new Question("Ce reprezintă Sarmizegetusa Regia?", new string[] { "O cetate romană din Italia", "Capitala Daciei" }, "b"),
        new Question("Cum au influențat romanii cultura dacilor?", new string[] { "Nu au avut nicio influență", "Au introdus limba latină și tehnici de construcție" }, "b"),
        new Question("Când a avut loc bătălia de la Rovine?", new string[] { "1395", "1600" }, "a"),
        new Question("Care era principalul zeu al romanilor?", new string[] { "Jupiter", "Zamolxis" }, "a"),
        new Question("Ce înseamnă 'romanizare'?", new string[] { "Adoptarea culturii și limbii romane", "Distrugerea cetăților dacice" }, "a"),
        new Question("Care este semnificația lui 1 Decembrie 1918?", new string[] { "Proclamarea independenței", "Marea Unire a României" }, "b"),
        new Question("Cum se numea sabia curbată a dacilor?", new string[] { "Gladius", "Falx" }, "b"),
        new Question("Ce au construit romanii pentru a proteja granițele Daciei?", new string[] { "Ziduri și fortificații", "Templuri și arene" }, "a"),
        new Question("Cine a fost Burebista?", new string[] { "Regele care a unit triburile dacice", "Împărat roman" }, "a"),
        new Question("Cum se numește procesul de unire a Moldovei și Țării Românești?", new string[] { "Unirea Principatelor", "Marea Unire" }, "a"),
        new Question("Ce a făcut Mihai Viteazul în 1600?", new string[] { "A construit Sarmizegetusa", "A unit pentru prima dată Țările Române" }, "b"),
        new Question("Ce reprezintă Columna lui Traian?", new string[] { "Lupta dintre romani și greci", "Victoria romanilor asupra dacilor" }, "b"),
        new Question("Cum se numește locul unde s-a proclamat Marea Unire?", new string[] { "Alba Iulia", "București" }, "a"),
        new Question("Cine a fost domnitorul care a obținut independența Țărilor Române față de otomani?", new string[] { "Mircea cel Bătrân", "Mihai Viteazul" }, "b"),
        new Question("Ce reprezintă cetățile dacice?", new string[] { "Centre fortificate pentru apărare", "Piețe comerciale" }, "a"),
        new Question("Care era materialul principal folosit de romani în construcții?", new string[] { "Lemn", "Beton roman" }, "b"),
        new Question("Ce s-a întâmplat în anul 101-102?", new string[] { "Primul război daco-roman", "Construirea cetății Sarmizegetusa" }, "a"),
        new Question("Cine era conducătorul roman în timpul războaielor daco-romane?", new string[] { "Traian", "Cezar" }, "a"),
        new Question("Cum se numește actul semnat la 1859 pentru Unirea Principatelor?", new string[] { "Tratatul de la Alba Iulia", "Convenția de la Paris" }, "b"),
        new Question("Ce simbolizează stindardul dacilor?", new string[] { "Forța și protecția zeilor", "Dreptatea și pacea" }, "a"),
        //Medii spre grele
        new Question("Ce domnitor a luptat în bătălia de la Vaslui?", new string[] { "Ștefan cel Mare", "Mihai Viteazul" }, "a"),
        new Question("Cum s-a numit prima lege scrisă a Daciei sub ocupație romană?", new string[] { "Codex Romanus", "Lex Provinciae" }, "b"),
        new Question("Ce oraș roman era cel mai important în Dacia?", new string[] { "Alba Iulia", "Ulpia Traiana Sarmizegetusa" }, "a"),
        new Question("Cum au contribuit dacii la formarea poporului român?", new string[] { "Prin tradiții și obiceiuri", "Prin adoptarea culturii romane" }, "a"),
        new Question("Cum se numește tratatul de pace semnat după războiul daco-roman din 102?", new string[] { "Tratatul de la Sarmizegetusa", "Pace între Traian și Decebal" }, "b"),
        new Question("Cum a fost influențată limba română de romanizare?", new string[] { "A devenit o limbă romanică", "Nu a fost influențată" }, "a"),
        new Question("Ce s-a întâmplat la 106 d.Hr.?", new string[] { "Dacia a fost cucerită de romani", "Mihai Viteazul a unit Țările Române" }, "a"),
        new Question("Cum se numește conducătorul care a apărat Țara Românească de invaziile otomane?", new string[] { "Mircea cel Bătrân", "Vlad Țepeș" }, "b"),
        new Question("Ce înseamnă 'autonomie'?", new string[] { "Libertatea unei țări de a se guverna singură", "Ocuparea unui teritoriu de către alt stat" }, "a"),
        new Question("Ce simbol apare pe stema Daciei?", new string[] { "Lupul dacic", "Soarele" }, "a"),
        new Question("Cum se numea în vechime ținutul de la nord de Dunăre ocupat de daci?", new string[] { "Tracia", "Dacia" }, "b"),
        new Question("Ce legi au fost introduse de romani în Dacia?", new string[] { "Legile romane (Lex Provinciae)", "Codurile dacice" }, "a"),
        new Question("Cum se numește strategia lui Ștefan cel Mare de a ataca trupele otomane?", new string[] { "Tactica hărțuirii", "Atacul frontal" }, "a"),
        new Question("Ce popoare migratoare au trecut prin fostul teritoriu al Daciei?", new string[] { "Romani și greci", "Huni, slavi, avari" }, "b"),
        new Question("Ce înseamnă „domnitor”?", new string[] { "Conducător al unui principat", "Lider al unei armate" }, "a"),
        new Question("Care a fost scopul războaielor daco-romane?", new string[] { "Cucerirea Daciei de către romani", "Independența Daciei față de romani" }, "a"),
        new Question("Cum era organizată societatea dacică?", new string[] { "În clase sociale cu roluri bine definite", "Fără reguli sau ierarhii" }, "a"),
        new Question("Cum se numește perioada în care au domnit Burebista și Decebal?", new string[] { "Evul Mediu timpuriu", "Epoca dacică" }, "b"),
        new Question("Ce construcție faimoasă a fost realizată de Apolodor din Damasc?", new string[] { "Columna lui Traian", "Podul peste Dunăre" }, "b"),
        new Question("Ce s-a întâmplat cu dacii după cucerirea romană?", new string[] { "Au migrat complet din Dacia", "Au fost romanizați treptat" }, "b"),
        new Question("Cum a fost desemnat Alexandru Ioan Cuza domnitor în ambele principate?", new string[] { "Prin dubla alegere", "Prin luptă militară" }, "a"),
        new Question("Ce bătălie faimoasă a avut loc la Posada?", new string[] { "Victoria lui Basarab I împotriva regelui Ungariei", "Lupta dintre Ștefan cel Mare și otomani" }, "a"),
        new Question("Cum se numea regiunea unde Mihai Viteazul a condus prima unire?", new string[] { "Țările Române (Moldova, Țara Românească, Transilvania)", "Imperiul Roman" }, "a"),
        new Question("Ce a simbolizat Proclamația de la Alba Iulia din 1 Decembrie 1918?", new string[] { "Eliberarea de sub stăpânirea otomană", "Marea Unire" }, "b"),
        new Question("Cum au influențat romanii drumurile din Dacia?", new string[] { "Au construit drumuri pavate pentru armată și comerț", "Nu au construit drumuri" }, "a"),
        new Question("Ce tip de arme foloseau dacii în luptele lor?", new string[] { "Tunuri și arbalete", "Săbii curbate și sulițe" }, "b"),
        new Question("Ce este un „sanctuar”?", new string[] { "Un loc sfânt folosit pentru ritualuri religioase", "O fortificație militară" }, "a"),
        new Question("Ce rol avea Burebista în consolidarea Daciei?", new string[] { "A unificat triburile dacice și a creat un stat puternic", "A fost un lider militar roman" }, "a"),
        new Question("Cum au influențat romanii arhitectura din Dacia?", new string[] { "Nu au adus schimbări arhitecturale", "Au introdus stilul roman cu arcuri și coloane" }, "b"),
        new Question("Ce s-a întâmplat cu Columna lui Traian după construcția sa?", new string[] { "A fost distrusă în Evul Mediu", "A rămas un simbol al victoriei romane" }, "b"),

        //Grele
        new Question("Care era religia dacilor înainte de romanizare?", new string[] { "Politeistă, cu Zamolxis ca zeu principal", "Monoteistă, cu Jupiter ca zeu principal" }, "a"),
        new Question("Cum a influențat romanizarea agricultura dacilor?", new string[] { "Nu au adus schimbări", "Au introdus noi unelte și metode de cultivare" }, "b"),
        new Question("Ce oraș roman a fost ridicat în sudul Daciei pentru administrație?", new string[] { "Tomis", "Apulum" }, "b"),
        new Question("Ce reprezintă \"Dacia Felix\"?", new string[] { "Dacia Romană, provincie prosperă", "O bătălie câștigată de romani" }, "a"),
        new Question("Cum s-a schimbat religia dacilor după romanizare?", new string[] { "Zeii romani au fost adoptați", "Religia lor a rămas neschimbată" }, "a"),
        new Question("Care a fost consecința războaielor daco-romane?", new string[] { "Distrugerea completă a Daciei", "Formarea provinciei Dacia" }, "b"),
        new Question("Ce înseamnă \"colonie romană\"?", new string[] { "Un teritoriu cucerit și organizat de romani", "O alianță între state" }, "a"),
        new Question("Cine era liderul dacilor în timpul cuceririi romane?", new string[] { "Burebista", "Decebal" }, "b"),
        new Question("Cum au influențat războaiele daco-romane arhitectura din Dacia?", new string[] { "Au fost construite orașe și drumuri romane", "Nu au avut influență asupra arhitecturii" }, "a"),
        new Question("Care este rolul istoriei pentru un popor?", new string[] { "Uitarea tradițiilor și adoptarea altor culturi", "Păstrarea identității și învățarea din trecut" }, "b"),


        //Geografie
        //Usoare
        new Question("Ce este o hartă?", new string[] { "O reprezentare grafică a unui teritoriu", "O carte" }, "a"),
        new Question("Care este capitala României?", new string[] { "Cluj-Napoca", "București" }, "b"),
        new Question("Care este cel mai mare ocean de pe Pământ?", new string[] { "Oceanul Atlantic", "Oceanul Pacific" }, "b"),
        new Question("Ce formă de relief reprezintă un munte?", new string[] { "O zonă joasă", "O zonă înaltă" }, "b"),
        new Question("Cum se numește râul care trece prin București?", new string[] { "Dunărea", "Mureș" }, "a"),
        new Question("Care este cel mai mare continent de pe Pământ?", new string[] { "Asia", "Africa" }, "a"),
        new Question("Ce este o deltă?", new string[] { "Un teritoriu uscat, cu multe munți", "O zonă de teren joasă, acoperită de ape, unde un râu se varsă într-un mare sau ocean" }, "b"),
        new Question("Ce este un ocean?", new string[] { "O mare foarte mare", "O apă dulce" }, "a"),
        new Question("Care este cel mai înalt munte din lume?", new string[] { "Munții Alpi", "Muntele Everest" }, "b"),
        new Question("Care dintre următoarele țări este vecină cu România?", new string[] { "Spania", "Ungaria" }, "b"),
        new Question("Care este cel mai mare lac din România?", new string[] { "Lacul Snagov", "Lacul Razim" }, "b"),
        new Question("Care este principalul fluviu care străbate Europa?", new string[] { "Dunărea", "Volga" }, "a"),
        new Question("Ce este un țărm?", new string[] { "Litoralul unei mări sau oceane", "O zonă montană" }, "a"),
        new Question("Ce continent este situat în întregime în emisfera sudică?", new string[] { "Africa", "Antarctica" }, "b"),
        new Question("Cum se numește țara din sudul României?", new string[] { "Bulgaria", "Serbia" }, "a"),
        new Question("În ce mare se varsă fluviul Dunărea?", new string[] { "Marea Adriatică", "Marea Neagră" }, "b"),
        new Question("Ce țară se află la vest de România?", new string[] { "Ucraina", "Ungaria" }, "b"),
        new Question("Ce formă de relief este Valea Prahovei?", new string[] { "Munte", "Depresiune" }, "b"),
        new Question("Ce este un continent?", new string[] { "O mare mare", "O mare masă de pământ care conține țări și oceane" }, "b"),
        new Question("Ce continent este cel mai apropiat de Polul Sud?", new string[] { "Africa", "Antarctica" }, "b"),
        
        //Usoare spre medii
        new Question("Care este capitala Franței?", new string[] { "Berlin", "Paris" }, "b"),
        new Question("Ce mare se află între Europa și Asia?", new string[] { "Marea Mediterană", "Marea Caspică" }, "b"),
        new Question("Care este țara vecină la est de România?", new string[] { "Ucraina", "Bulgaria" }, "a"),
        new Question("Ce formă de relief este Dunărea?", new string[] { "Fluviu", "Munte" }, "a"),
        new Question("Ce este un orizont geografic?", new string[] { "O linie imaginară care împărțea Pământul în două părți", "O zonă geografică" }, "a"),
        new Question("Care este cel mai mare desert de pe Pământ?", new string[] { "Sahara", "Gobi" }, "a"),
        new Question("Ce țară este cunoscută pentru piramidele sale?", new string[] { "Egipt", "India" }, "a"),
        new Question("Care este cel mai mare stat din lume?", new string[] { "China", "Rusia" }, "b"),
        new Question("Ce este un arhipelag?", new string[] { "Un grup de insule", "Un tip de munte" }, "a"),
        new Question("Care este capitala Italiei?", new string[] { "Roma", "Milano" }, "a"),
        new Question("Cum se numește capitala Japoniei?", new string[] { "Beijing", "Tokyo" }, "b"),
        new Question("Ce mare este situată între Europa și America de Nord?", new string[] { "Marea Mediterană", "Marea Labrador" }, "b"),
        new Question("Ce țară are cel mai mare număr de insule?", new string[] { "Grecia", "Suedia" }, "b"),
        new Question("Cum se numește lacul situat la granița între România și Ucraina?", new string[] { "Lacul Razim", "Lacul Victoria" }, "a"),
        new Question("Care este capitala Spaniei?", new string[] { "Madrid", "Barcelona" }, "a"),
        new Question("Care este cel mai adânc lac din lume?", new string[] { "Lacul Baikal", "Lacul Caspic" }, "a"),
        new Question("Ce se află la nord de România?", new string[] { "Ucraina", "Bulgaria" }, "a"),
        new Question("Care este cel mai important fluviu din Africa?", new string[] { "Nil", "Congo" }, "a"),
        new Question("Cum se numește țara cunoscută pentru Marele Zid?", new string[] { "Coreea de Nord", "China" }, "b"),
        new Question("Ce continent are cea mai mare densitate a populației?", new string[] { "Asia", "Europa" }, "a"),

        //Medii
        new Question("Ce este o insulă?", new string[] { "O zonă de uscat înconjurată de apă", "O zonă acoperită cu păduri" }, "a"),
        new Question("În ce munți se află vârful Făgăraș?", new string[] { "Munții Carpați", "Munții Apuseni" }, "a"),
        new Question("Care este cel mai mare râu din Europa?", new string[] { "Volga", "Dunărea" }, "a"),
        new Question("Cum se numește marele deșert din Africa?", new string[] { "Sahara", "Kalahari" }, "a"),
        new Question("Care este cel mai înalt vârf din România?", new string[] { "Vârful Moldoveanu", "Vârful Omu" }, "a"),
        new Question("Care sunt principalele unități de relief din România?", new string[] { "Munți, dealuri, câmpii", "Deșerturi și insule" }, "a"),
        new Question("Care sunt continentele care se află în emisfera sudică?", new string[] { "Africa, Antarctica, Oceania", "Europa, Asia, America de Nord" }, "a"),
        new Question("Care este denumirea regiunii în care se află orașul Constanța?", new string[] { "Dobrogea", "Moldova" }, "a"),
        new Question("Cum se numește marele deșert din Australia?", new string[] { "Deșertul Gobi", "Deșertul Simpson" }, "b"),
        new Question("Care este cel mai mare râu din America de Sud?", new string[] { "Amazon", "Mississippi" }, "a"),
        new Question("În ce țară se află Muntele Kilimanjaro?", new string[] { "Uganda", "Tanzania" }, "b"),
        new Question("Cum se numește capitala Japoniei?", new string[] { "Beijing", "Tokyo" }, "b"),
        new Question("Ce mare este situată între Europa și America de Nord?", new string[] { "Marea Mediterană", "Marea Labrador" }, "b"),
        new Question("Ce țară are cel mai mare număr de insule?", new string[] { "Grecia", "Suedia" }, "b"),
        new Question("Cum se numește lacul situat la granița între România și Ucraina?", new string[] { "Lacul Razim", "Lacul Victoria" }, "a"),
        new Question("Care este capitala Spaniei?", new string[] { "Madrid", "Barcelona" }, "a"),
        new Question("Care este cel mai adânc lac din lume?", new string[] { "Lacul Baikal", "Lacul Caspic" }, "a"),
        new Question("Ce se află la nord de România?", new string[] { "Ucraina", "Bulgaria" }, "a"),
        new Question("Care este cel mai important fluviu din Africa?", new string[] { "Nil", "Congo" }, "a"),
        new Question("Cum se numește țara cunoscută pentru Marele Zid?", new string[] { "Coreea de Nord", "China" }, "b"),

        //Medii spre grele
        new Question("Care este capitala statului Canada?", new string[] { "Toronto", "Ottawa" }, "b"),
        new Question("Ce formă de relief se află la sud de România?", new string[] { "Munții Carpați", "Câmpiile Dunării" }, "b"),
        new Question("Care este cel mai mare stat din Africa?", new string[] { "Sudan", "Algeria" }, "b"),
        new Question("Care este cel mai mare lac de apă dulce din lume?", new string[] { "Lacul Baikal", "Lacul Superior" }, "a"),
        new Question("Ce țară este situată între Italia și Franța?", new string[] { "Spania", "Monaco" }, "b"),
        new Question("Care este cel mai mare oraș din Australia?", new string[] { "Sydney", "Melbourne" }, "a"),
        new Question("În ce țară se află Insulele Galapagos?", new string[] { "Ecuador", "Chile" }, "a"),
        new Question("Care este cel mai înalt vârf din Alpi?", new string[] { "Mont Blanc", "Matterhorn" }, "a"),
        new Question("Care este cel mai adânc punct de pe Pământ?", new string[] { "Groapa Marianelor", "Marea Caspică" }, "a"),
        new Question("Ce mare se află între Italia și Albania?", new string[] { "Marea Adriatică", "Marea Ioniană" }, "a"),
        new Question("Care este cel mai lung râu din Asia?", new string[] { "Gange", "Yangtze" }, "b"),
        new Question("Ce este un „tropot”?", new string[] { "O formă de relief", "Un vânt de mare viteză" }, "b"),
        new Question("Care este capitala Egiptului?", new string[] { "Cairo", "Alexandria" }, "a"),
        new Question("Care este cel mai mare oraș din Statele Unite ale Americii?", new string[] { "New York", "Los Angeles" }, "a"),
        new Question("Ce ocean se află între America de Sud și Africa?", new string[] { "Oceanul Atlantic", "Oceanul Indian" }, "a"),
        new Question("Cum se numește deșertul aflat în sudul Americii de Nord?", new string[] { "Deșertul Sonora", "Deșertul Atacama" }, "a"),
        new Question("În ce țară se află Muntele Fuji?", new string[] { "Coreea de Sud", "Japonia" }, "b"),
        new Question("Care este cel mai mare aeroport din lume ca suprafață?", new string[] { "Aeroportul din Beijing", "Aeroportul din Dubai" }, "a"),
        new Question("Care este cel mai lung râu din Europa?", new string[] { "Volga", "Dunărea" }, "a"),
        new Question("Cum se numește principala insulă din Grecia?", new string[] { "Creta", "Rodos" }, "a"),
        new Question("Care este cel mai mare lac din Africa?", new string[] { "Lacul Victoria", "Lacul Tanganyika" }, "a"),
        new Question("În ce țară se află cea mai mare pădure tropicală?", new string[] { "Brazilia", "India" }, "a"),
        new Question("Ce mare se află între Franța și Marea Britanie?", new string[] { "Marea Nordului", "Canalul Mânecii" }, "b"),
        new Question("Care este țara cu cel mai mare număr de munți?", new string[] { "India", "Nepal" }, "b"),
        new Question("Care este cel mai înalt vulcan din lume?", new string[] { "Muntele Kilimanjaro", "Ojos del Salado" }, "b"),
        new Question("Care este cel mai lung râu din America de Sud?", new string[] { "Amazon", "Orinoco" }, "a"),
        new Question("Care este cel mai adânc lac din Africa?", new string[] { "Lacul Tanganyika", "Lacul Victoria" }, "a"),
        new Question("Care este cel mai mare stat din America de Sud?", new string[] { "Argentina", "Brazilia" }, "b"),
        new Question("În ce țară se află Marele Canal din Veneția?", new string[] { "Italia", "Grecia" }, "a"),
        new Question("Care este țara cu cel mai mare număr de insule?", new string[] { "Suedia", "Indonezia" }, "b"),

        //Grele
        new Question("Care este cel mai mare deșert rece din lume?", new string[] { "Deșertul Gobi", "Deșertul Antarctic" }, "b"),
        new Question("Ce țară are cea mai mare zonă montană?", new string[] { "Elveția", "Nepal" }, "b"),
        new Question("Care este cel mai înalt vârf din Himalaya?", new string[] { "Muntele Everest", "K2" }, "a"),
        new Question("Care este cel mai mare râu din Africa?", new string[] { "Niger", "Nil" }, "b"),
        new Question("Ce țară are cel mai mare număr de lacuri?", new string[] { "Canada", "Finlanda" }, "a"),
        new Question("Care este cel mai adânc punct de pe Pământ?", new string[] { "Groapa Marianelor", "Groapa Tonga" }, "a"),
        new Question("Care este cel mai lung râu din Asia?", new string[] { "Yangtze", "Gange" }, "a"),
        new Question("Care este cel mai mare ocean din lume?", new string[] { "Oceanul Atlantic", "Oceanul Pacific" }, "b"),
        new Question("În ce țară se află cel mai mare vulcan activ din lume?", new string[] { "Japonia", "Statele Unite ale Americii" }, "b"),
        new Question("Care este cel mai înalt vârf din Alpi?", new string[] { "Mont Blanc", "Matterhorn" }, "a"),

    };

    // List to store all the child GameObjects of the triggerObject
    private List<Transform> childrenList = new List<Transform>();
    
    
    private List<Transform> damageObjects = new List<Transform>();


    private string filePath;
    
    private string filePath2;

    private void Start()
    {
        // Ensure the taskTitleMap starts hidden

        filePath = Application.persistentDataPath + "/save.txt";
        filePath2 = Application.persistentDataPath + "/save_score.txt";
        if (taskTitleMap != null)
        {
            taskTitleMap.SetActive(false);
            question.SetActive(false);
            questionText.SetActive(false);
        }

        if (button1 != null)
        {
            button1.onClick.AddListener(OnButton1Click);
            button1.GetComponentInChildren<TextMeshProUGUI>().text = ""; // Set button text
        }

        if (button2 != null)
        {
            button2.onClick.AddListener(OnButton2Click);
            button2.GetComponentInChildren<TextMeshProUGUI>().text = ""; // Set button text
        }

        // Populate the children list at the start
        List<string> namesFromFile = ReadChildNamesFromFile();
        for (int i = 0; i < triggerObject.transform.childCount; i++)
        {
            Transform childTransform = triggerObject.transform.GetChild(i);

            // Check if the child's name is not in the list
            if (!namesFromFile.Contains(childTransform.name))
            {
                childrenList.Add(childTransform);
                Debug.Log($"Added {childTransform.name} to childrenList.");
            }
            else
            {
                Debug.Log($"Skipped {childTransform.name} because it's in the file.");
            }
        }
        
        for (int i = 0; i < damageObject.transform.childCount; i++)
        {
            Transform childTransform = damageObject.transform.GetChild(i);

            // Check if the child's name is not in the list
            if (!namesFromFile.Contains(childTransform.name))
            {
                damageObjects.Add(childTransform);
                Debug.Log($"Added {childTransform.name} to damageObjects.");
            }
            else
            {
                childTransform.gameObject.SetActive(false);
                Debug.Log($"Skipped {childTransform.name} because it's in the file.");
            }
        }
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        } 
        if (!File.Exists(filePath2))
        {
            File.Create(filePath2).Close();
        } 
        
        int loadedValue = LoadInt();
        Money.text = loadedValue.ToString();
        // PrintChildNames();
    }
    
    

    private void Update()
    {
        // Check if the taskTitleMap is close enough to any child object of the triggerObject
        if (IsCloseToAnyChild())
        {
            // If the taskTitleMap is within proximity, show it
            if (taskTitleMap != null)
            {
                taskTitleMap.SetActive(true);
            }

            // Check for clicks while the taskTitleMap is active
            if (Input.GetMouseButtonDown(0)) // Left mouse button or tap
            {
                HandleClick();
                question.SetActive(true);
                questionText.SetActive(true);
            }
        }
        else
        {
            // If the taskTitleMap is too far, hide it
            if (taskTitleMap != null)
            {
                taskTitleMap.SetActive(false);
                question.SetActive(false);
                questionText.SetActive(false);
            }
        }
    }

    private bool IsCloseToAnyChild()
    {
        // Get the position of the taskTitleMap (this GameObject)
        Vector2 taskTitleMapPosition = transform.position;

        int i=0;
        // Loop through the children stored in the list
        foreach (Transform childTransform in childrenList)
        {
            // Get the position of the current child
            Vector2 childPosition = childTransform.position;

            // Calculate the distance between the taskTitleMap and this child object
            float distance = Vector2.Distance(taskTitleMapPosition, childPosition);
            
            // If any child is within the proximity threshold, return true
            if (distance <= proximityThreshold)
            {
                child = childTransform;
                childDmg = damageObjects[i];
                return true;
            }

            i++;
        }

        // If none of the children are close enough, return false
        return false;
    }

    private void HandleClick()
    {
        // Extract the index from the name of the nearest child
        Debug.Log(0);
        string childName = child.name;
        int questionIndex = int.Parse(childName) - 1; // Assuming child names are "1", "2", etc.
        Debug.Log(2);
        // Ensure the question index is valid
        if (questionIndex >= 0 && questionIndex < questions.Length)
        {
            Question currentQuestion = questions[questionIndex];
            
            // Set the question text and button answers based on the question data
            Debug.Log(3);
            taskText.text = currentQuestion.question;
            Debug.Log(1);
            button1.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[0];
            button2.GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.answers[1];

            // Resize buttons to fit text
            ResizeButtonToFitText(button1);
            ResizeButtonToFitText(button2);
        }
    }

    private void ResizeButtonToFitText(Button button)
    {
        // Ensure that the button resizes according to the text inside it
        var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        var layoutElement = button.GetComponent<LayoutElement>();

        if (layoutElement != null)
        {
            // Set the preferred width to auto-adjust based on the text
            layoutElement.preferredWidth = -1;
            layoutElement.preferredHeight = -1;
        }

        // Optionally, use ContentSizeFitter if you want dynamic resizing
        var contentSizeFitter = button.GetComponent<ContentSizeFitter>();
        if (contentSizeFitter != null)
        {
            contentSizeFitter.SetLayoutHorizontal();
            contentSizeFitter.SetLayoutVertical();
        }
    }

    private void OnButton1Click()
    {
        CheckAnswer(0); // Button 1 corresponds to the first answer
    }

    private void OnButton2Click()
    {
        CheckAnswer(1); // Button 2 corresponds to the second answer
    }

    private void CheckAnswer(int buttonIndex)
    {
        // Find out which button was clicked and check if it's the correct answer
        string correctAnswer = questions[int.Parse(child.name) - 1].correctAnswer;
        Button clickedButton = buttonIndex == 0 ? button1 : button2;

        // Check if the clicked answer is correct
        if ((buttonIndex == 0 && correctAnswer == "a") || (buttonIndex == 1 && correctAnswer == "b"))
        {
            clickedButton.GetComponent<Image>().color = Color.green; // Set button color to green for correct answer

            // Hide the child GameObject after a correct answer with a delay
            StartCoroutine(WaitAndRemoveChild(child,childDmg, clickedButton));
        }
        else
        {
            clickedButton.GetComponent<Image>().color = Color.red; // Set button color to red for wrong answer

            // Optionally reset the color after a short delay
            StartCoroutine(ResetButtonColorAfterDelay(clickedButton));
        }
    }
    
    public void SaveInt(int value)
    {
        try
        {
            // Write the integer to the file
            File.WriteAllText(filePath2, value.ToString());
            Debug.Log($"Integer {value} saved to {filePath}");
        }
        catch (IOException ex)
        {
            Debug.LogError("Error saving integer: " + ex.Message);
        }
    }
    public int LoadInt()
    {
        try
        {
            // Check if the file exists
            if (File.Exists(filePath2))
            {
                string content = File.ReadAllText(filePath2);
                int value;
            
                // Parse the string to an integer
                if (int.TryParse(content, out value))
                {
                    Debug.Log($"Integer {value} loaded from {filePath2}");
                    return value;
                }
                else
                {
                    Debug.LogWarning($"File content is not a valid integer: {content}");
                }
            }
            else
            {
                Debug.LogWarning($"File not found at {filePath}");
            }
        }
        catch (IOException ex)
        {
            Debug.LogError("Error loading integer: " + ex.Message);
        }

        return 0; // Default value if file doesn't exist or parsing fails
    }

    private IEnumerator WaitAndRemoveChild(Transform child,Transform dmg, Button clickedButton)
    {
        // Wait for 3 seconds before removing the child
        yield return new WaitForSeconds(3f);

        // Disable the GameObject after 3 seconds
        child.gameObject.SetActive(false);

        // Remove the child from the list
        childrenList.Remove(child);

        dmg.gameObject.SetActive(false);

        int monys = Int32.Parse(Money.text) + 10;

        Money.text = monys.ToString();
        
        SaveInt(monys);
        
        SaveChildNameToFile(child.name);

        // Reset the button color after a short delay
        yield return new WaitForSeconds(1f);  // Waiting for a moment before resetting the button color
        ResetButtonColor(clickedButton);
    }
    
    private void SaveChildNameToFile(string childName)
    {
        // Append the child's name to the file with a new line
        File.AppendAllText(filePath, childName + "\n");
        Debug.Log($"Child name saved to file: {childName}");
    }
    
    public void PrintChildNames()
    {
        Debug.Log("Aici merge!");
        List<string> names = ReadChildNamesFromFile();
        foreach (string name in names)
        {
            Debug.Log("Child name: " + name);
        }
    }
    
    public List<string> ReadChildNamesFromFile()
    {
        List<string> childNames = new List<string>();

        try
        {
            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Add each line to the list
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line)) // Ignore empty or whitespace lines
                    {
                        childNames.Add(line.Trim());
                    }
                }

                Debug.Log("Read child names from file successfully.");
            }
            else
            {
                Debug.LogWarning("File not found: " + filePath);
            }
        }
        catch (IOException ex)
        {
            Debug.LogError("Error reading file: " + ex.Message);
        }

        return childNames;
    }

    private IEnumerator ResetButtonColorAfterDelay(Button clickedButton)
    {
        // Wait for 1 second before resetting the color
        yield return new WaitForSeconds(1f);
        ResetButtonColor(clickedButton);
    }

    private void ResetButtonColor(Button clickedButton)
    {
        // Reset button color to white or default color
        clickedButton.GetComponent<Image>().color = Color.white;
    }

    // Question class to hold the data
    public class Question
    {
        public string question;
        public string[] answers;
        public string correctAnswer;

        public Question(string question, string[] answers, string correctAnswer)
        {
            this.question = question;
            this.answers = answers;
            this.correctAnswer = correctAnswer;
        }
    }
}
