U S E   [ I n M e m o r y _ V a u l t L i f e S t a g e ]  
 G O  
 / * * * * * *   O b j e c t :     S t o r e d P r o c e d u r e   [ d b o ] . [ I s U s e r W i n n e r ]         S c r i p t   D a t e :   2 0 1 4 - 1 2 - 0 4   1 1 : 1 0 : 4 5   A M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 C R E A T E   P R O C E D U R E   [ d b o ] . [ I s U s e r W i n n e r ]   @ G a m e I D   I N T  
 	 , @ m e m b e r I d   I N T  
 	  
 	 A S    
 	 B E G I N  
 S E L E C T   w i n n e r  
 F R O M   p r o d u c t P l a y e d   p  
  
 I N N E R   J O I N   m e m b e r i n g a m e   m   O N   m . m e m b e r i n g a m e I d   = p . m e m b e r i n g a m e I d    
  
 w h e r e   m . m e m b e r i d   =   @ m e m b e r I d   a n d   m . g a m e I d   =   @ g a m e I d  
  
 e n d    
 