using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HanoiTower
{

    public class HanoiTower{
        public int DiscsCount { get; private set; }
        public int MovesCount { get; private set; }
        public Stack<int> From { get; private set; }
        public Stack<int> To { get; private set; }
        public Stack<int> Auxiliary { get; private set; }
        public event EventHandler<EventArgs> MoveCompleted;
    
    //Pega o numero de discos e os coloca em ordem
    public HanoiTower(int discs){
        DiscsCount = discs;
        From = new Stack<int>();
        To = new Stack<int>();
        Auxiliary = new Stack<int>();
        for (int i = 1; i <= discs; i++){
            int size = discs - i + 1;
            From.Push(size);
            }
        }

    public void Start(){
        Move(DiscsCount, From, To, Auxiliary);
        }

    //Metodo recursivo
    public void Move(int discs, Stack<int> from, Stack<int> to,Stack<int> auxiliary){
        if (discs > 0){
            //Movendo todos os discos exeto 1 de FROM exceto o maior para a pilha AUXILIARY usando TO como suporte
            Move(discs - 1, from, auxiliary, to);
            //Movendo o maior disco de FROM para TO
            to.Push(from.Pop());
            MovesCount++    ;
            //Dispara evento
            MoveCompleted?.Invoke(this, EventArgs.Empty);
            //Movendo todos os discos exeto 1 de AUXILIARY exceto o maior para a pilha TO usando FROM como suporte
            Move(discs - 1, auxiliary, to, from);
            }
        }
    }
}

    

