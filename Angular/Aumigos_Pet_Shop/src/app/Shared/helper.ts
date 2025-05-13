export class Helper {
  static selectActiveHandler(element: HTMLEmbedElement) {
    const itens = document.querySelectorAll(".activatable");
    const target = element;
    itens.forEach(item => {
      item.classList.remove("active");
    })
    target.classList.add("active");
  }

  static formatPhoneHelper(phone: string){
    if(!phone){
      return null;
    }

    return phone.replace(/^(\d{2})(\d{5})(\d{4})$/, "($1) $2-$3");
  }
}