export class Handlers{
    static selectActiveHandler(element: MouseEvent) {
        const itens = document.querySelectorAll(".activatable");
        const target = element.currentTarget as HTMLEmbedElement;
        itens.forEach(item => {
          item.classList.remove("active");
        })
        target.classList.add("active");
      }
}