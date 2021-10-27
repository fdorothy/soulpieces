var story = null
var root = null

function clear()
{
  while (root.firstChild) {
    root.removeChild(root.firstChild);
  }
}

function addText(txt)
{
  const elem = document.createElement('p')
  elem.classList.add('fade_in')
  elem.innerHTML = txt
  root.appendChild(elem)
}

function addImage(url)
{
  const elem = document.createElement('img')
  elem.classList.add('fade_in')
  elem.classList.add('center')
  elem.src = url
  root.appendChild(elem)
}

function addChoice(index, text)
{
  const elem = document.createElement('button')
  elem.classList.add('fade_in')
  elem.classList.add('center')
  elem.innerHTML = text
  elem.onclick = function() {selectChoice(index)}
  root.appendChild(elem)
}

function selectChoice(index)
{
  clear()
  story.ChooseChoiceIndex(index)
  continueStory(story)
}

function continueStory(story)
{
  const text = story.Continue()
  const found = text.match(/\[\[(.*)\]\]/)
  if (found !== null) {
    addImage(found[1])
  } else {
    addText(text)
  }
  if (story.canContinue) {
    setTimeout(() => continueStory(story), 400)
  } else {
    for (let i=0; i<story.currentChoices.length; i++) {
      console.log(`${i} ${story.currentChoices[i].text}`)
      addChoice(i, story.currentChoices[i].text)
    }
  }
}

function main()
{
  root = document.getElementById('root')

  fetch('story.ink.json')
    .then(response => response.text())
    .then((storyContent) => {
      console.log(storyContent)
      story = new inkjs.Story(storyContent)
      continueStory(story)
    })
}
