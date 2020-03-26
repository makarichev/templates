<script>
  import { onMount, onDestroy, setContext, getContext } from "svelte";
  import { user, reglament, route, socket, toAsts} from "./store.js";
  import { asRouterLink } from "./Shared/actions.js";


import Toast from './Shared/Toast.svelte'

  
  export let components = [];

  let s = socket.subscribe(x => {if (x) toAsts.warning(x.data)})
  onDestroy(s);

  
  setContext("name", 'Template');
  setContext("filter", null);


</script>

<svelte:head>
  <title>{getContext("name")}</title>
</svelte:head>

{#if components}
  <svelte:component
    this={components[0]}
    components={components.slice(1)}
	reglament = {$reglament}
	user = {$user}
    />
{/if}

<Toast></Toast>


