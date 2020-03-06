<script>
  import { onMount, onDestroy, setContext } from "svelte";
  
  

  
	export let name;
	export let context;
	export let component;

	setContext('name', name);

	


</script>

<style>

  h1 {
    color: #ff3e00;
    text-transform: uppercase;
    font-size: 4em;
    font-weight: 100;
  }

</style>

<svelte:head>
  <title>{name}</title>
  <base href="/"><base>
</svelte:head>

{#if component}
	<svelte:component this={component} route={context} {name}></svelte:component>
{/if}
