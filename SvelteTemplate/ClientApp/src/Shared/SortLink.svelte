<script>
    import { createEventDispatcher } from 'svelte';
    
    const dispatch = createEventDispatcher();

    export let name = null;
    export let sort = null;

    function onSorted() {
        let asc = "ASC";
        if (sortname === name)
            asc = sortasc === "ASC"? "DESC": "ASC"
        
        dispatch('sorted', `${name} ${asc}`);
    }
    
    let sortname = name, sortasc = "ASC"

    $: {
        if (sort && name) {
            let current = sort.split(' ')
            if (current[0]) sortname = current[0];
            if (current.length > 1) sortasc = current[1]; else sortasc = "ASC"
        }
        
    }

</script>

<style>
    a {white-space: nowrap}
</style>

<a href="/" on:click|preventDefault={onSorted}>
    <slot></slot>
    {#if sortname === name} 
        {#if sortasc === 'DESC'}
        <i class="fa fa-chevron-up"></i>
        {:else}
        <i class="fa fa-chevron-down"></i>
        {/if}
    {/if}
    
</a>


