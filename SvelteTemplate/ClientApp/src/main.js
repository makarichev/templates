import App from './App.svelte';
import Home from './Components/Home.svelte';
import About from './Components/About.svelte';
 import Mols from './Components/Mols/Mols.svelte';
 import Mol from './Components/Mols/Mol.svelte';
 import New from './Components/Mols/New.svelte';
 import Common from './Components/Mols/Common.svelte';
 import Private from './Components/Mols/Private.svelte';
 import Photo from './Components/Mols/Photo.svelte';
 import Access from './Components/Mols/Access.svelte';
 import Education from './Components/Mols/Education.svelte';
 import Hist from './Components/Mols/Hist.svelte';


import {route} from './store.js';


import page from 'page';


const app = new App({
	target: document.body,
	props: {
	}
});

function setRoute(path)  {


	page(path, (ctx, next) => {
		route.set(ctx);
		ctx.state.Hist = false;
		let components = [...arguments].slice(1); 
		app.$set({components});
	})
}


setRoute('/', Home)
setRoute('/home', Home)
setRoute('/about', About)
setRoute('/mols', Mols)
setRoute('/mols/new', New)
setRoute('/mols/:id', Mol, Common)
setRoute('/mols/:id/common', Mol, Common)
setRoute('/mols/:id/private', Mol, Private)
setRoute('/mols/:id/photo', Mol, Photo)
setRoute('/mols/:id/access', Mol, Access)
setRoute('/mols/:id/education', Mol, Education)
setRoute('/mols/:id/hist', Mol, Hist)


page.start();




export default app;