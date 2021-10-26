function OnClickNext()
{
  console.log('next')
}

function OnClickPrev()
{
  console.log('prev')
}

function showParagraph(elem)
{
  return () => {
    elem.className += " fade_in"
  }
}

function main()
{
  const elem = document.getElementsByTagName('h1')
  for (let i=0; i<elem.length; i++)
    elem[i].className = "title fade_in"
  const paragraphs = document.getElementsByTagName('p')
  for (let i=0; i<paragraphs.length; i++) {
    setTimeout(showParagraph(paragraphs[i]), 1000 * i)
  }
}

setTimeout(main, 2000)

console.log('hello, world')
