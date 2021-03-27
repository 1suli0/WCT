# WCT API

Napomena:

- Nakon kreiranja docker kontejnera Swagger dokumentaciji API-ja se moze pristupiti putem http://localhost:8080/swagger/index.html
- Authentificacija se vrsi tako da se Authorization header postavi u format "Bearer token"
- U bazi vec postoji kreiran user sa pristupnim podacima 
    - username: admin@test.com 
    - password: test
- Kada se govori o mapiranju iz DTOa u model i obratno, ono je radjeno "rucno", tj. koristena je <a href ="https://github.com/cezarypiatek/MappingGenerator">ekstenzija</a> za Visual studio 2019 da se generise odgovarajuci kod. Drugaciji pristup ovom bi bio koristenje AutoMapper biblioteke
- Da se radi o produkcijskom apiju bilo bi potrebno dodatno obratiti paznju na sigurnosni aspekt aplikacije, te implementirati jos nekoliko stvari, od kojih se mogu izdvojiti:
  - caching -> da se rastereti baza podataka
  - rate limiting (throttling) -> da se ogranici broj requestova po jedinici vremena sa odredjene IP adrese, da se rastereti web server
  - pagination -> da se ogranici kolicina podataka unutar requesta
