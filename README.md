# Trees for Ants
A game about planting (small) trees, changing the weather and harvesting fruits.

## Design keuzes
- redenen om een miniatuurwereld te gebruiken:
  - geen locomotion nodig, wat vaak prettiger is voor mensen met weinig VR ervaring
  - meer interactie mogelijk 'binnen handbereik', minder afhankelijk van wijzen naar dingen
  - mogelijkheid voor Mixed Reality versie, zodat je niet volledig afgesloten bent voor je omgeving
  - het stereo/diepte-effect van VR komt het best tot zijn recht bij objecten dichterbij
- de zonlicht/regen/schaduw meters worden gereset bij het veranderen van groeifase voor een 'level up' effect
- appelbomen hebben meer regen nodig, sinaasappelbomen meer zonlicht, zodat de keuze voor het boomtype interessanter wordt
- bomen met meer ruimte produceren meer fruit zodat betere plaatsing een hogere score kan opleveren
- bomen hebben minder nodig als ze volgroeid zijn zodat de speler meer tijd heeft om het fruit te oogsten
- de locomotion functionaliteit van de VR template staat nog aan voor extra flexibiliteit, deze zou uitgezet kunnen worden voor demonstraties met genoeg loopruimte
- de bestanden van de Unity VR template zitten nog allemaal in het project. Normaal gesproken zou ik beginnen met een leeg project en dan alleen de assets importeren die ik nodig had, maar omdat dit een simpel projectje is heb ik dat nu niet gedaan om tijd te besparen