U S E   [ I n M e m o r y _ V a u l t L i f e S t a g e ]  
 G O  
 / * * * * * *   O b j e c t :     S t o r e d P r o c e d u r e   [ d b o ] . [ P e r s i s t U s e r I n t e r a c t i o n ]         S c r i p t   D a t e :   2 0 1 4 - 1 2 - 0 4   1 1 : 0 6 : 2 2   A M   * * * * * * /  
 S E T   A N S I _ N U L L S   O N  
 G O  
 S E T   Q U O T E D _ I D E N T I F I E R   O N  
 G O  
 C R E A T E   P R O C E D U R E   [ d b o ] . [ P e r s i s t U s e r I n t e r a c t i o n ]   @ P r o d u c t I n G a m e I D   I N T  
 	 , @ m e m b e r I n G a m e I D   I N T  
 	 , @ O f f S e t   I N T  
 	 W I T H   N A T I V E _ C O M P I L A T I O N ,   S C H E M A B I N D I N G ,   E X E C U T E   A S   O W N E R    
 A S    
 B E G I N   A T O M I C    
   W I T H   ( T R A N S A C T I O N   I S O L A T I O N   L E V E L   =   S N A P S H O T ,   L A N G U A G E   =   N ' u s _ e n g l i s h ' )  
  
  
 	 - -   S E T   N O C O U N T   O N   a d d e d   t o   p r e v e n t   e x t r a   r e s u l t   s e t s   f r o m  
 	 - -   i n t e r f e r i n g   w i t h   S E L E C T   s t a t e m e n t s .  
 	  
  
 	 - -   I n s e r t   s t a t e m e n t s   f o r   p r o c e d u r e   h e r e  
 	 I N S E R T   I N T O   d b o . P r o d u c t P l a y e d   (  
 	 	 P r o d u c t I n G a m e I D  
 	 	 , M e m b e r I n G a m e I D  
 	 	 , C l i c k I n t e r v a l  
 	 	 , D a t e I n s e r t e d  
 	 	 , W i n n e r  
 	 	 )  
 	 V A L U E S   (  
 	 	 @ P r o d u c t I n G a m e I D  
 	 	 , @ m e m b e r I n G a m e I D  
 	 	 , @ O f f S e t  
 	 	 , G E T D A T E ( )  
 	 	 , 0  
 	 	 )  
 E N D  
  
  
 