u s i n g   S y s t e m ;  
 u s i n g   S w i n G a m e S D K ;  
  
 n a m e s p a c e   M y G a m e  
 {  
         p u b l i c   c l a s s   G a m e M a i n  
         {  
                 p u b l i c   s t a t i c   v o i d   M a i n ( )  
                 {  
                         / / O p e n   t h e   g a m e   w i n d o w  
                         S w i n G a m e . O p e n G r a p h i c s W i n d o w ( " G a m e M a i n " ,   8 0 0 ,   6 0 0 ) ;  
                         S w i n G a m e . S h o w S w i n G a m e S p l a s h S c r e e n ( ) ;  
                          
                         / / R u n   t h e   g a m e   l o o p  
                         w h i l e ( f a l s e   = =   S w i n G a m e . W i n d o w C l o s e R e q u e s t e d ( ) )  
                         {  
                                 / / F e t c h   t h e   n e x t   b a t c h   o f   U I   i n t e r a c t i o n  
                                 S w i n G a m e . P r o c e s s E v e n t s ( ) ;  
                                  
                                 / / C l e a r   t h e   s c r e e n   a n d   d r a w   t h e   f r a m e r a t e  
                                 S w i n G a m e . C l e a r S c r e e n ( C o l o r . W h i t e ) ;  
                                 S w i n G a m e . D r a w F r a m e r a t e ( 0 , 0 ) ;  
                                  
                                 / / D r a w   o n t o   t h e   s c r e e n  
                                 S w i n G a m e . R e f r e s h S c r e e n ( 6 0 ) ;  
                         }  
                 }  
         }  
 } 