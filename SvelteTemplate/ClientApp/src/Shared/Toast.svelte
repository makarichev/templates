<script>
    import {onMount, onDestroy} from 'svelte'
    import {toAsts} from '../store'
    import {asDate} from './actions.js'
   

    let messages = [];

    let s = toAsts.subscribe(x => {
        if (x) {
            messages = [...messages, x]
            setTimeout(y => 
                window.$(`#${x.date.valueOf()}`)
                    .on('hidden.bs.toast', function (e) {
                        setTimeout(() => messages = messages.filter(m => m.date.valueOf() !== e.target.id), 100)
                    })
                    .toast({delay:x.delay, autohide: x.autohide})
                    .toast('show')
            , 100);      
        }
    })
    onDestroy(s)

</script>

<div aria-live="polite" aria-atomic="true">
  <!-- Position it -->
  <div style="position: absolute; bottom: 4px; right: 4px;">


    {#each messages as item}

    <!-- Then put toasts within -->
    <div id="{item.date.valueOf()}" class="toast" role="alert" aria-live="assertive" aria-atomic="true" style="min-width:300px">
      <div class="toast-header bg-primary text-light"
        class:bg-primary={item.type == 0}
        class:bg-danger={item.type == 1}
        class:bg-warning={item.type == 2}
        class:bg-success={item.type == 3}
      >
        
        <strong class="mr-auto"><i class="fa" 
            class:fa-info-circle={item.type == 0 || item.type == 3}
            class:fa-exclamation-circle={item.type == 1 || item.type == 2}
        aria-hidden="true"></i></strong>
        <small class="mr-2" use:asDate={item.date}></small>
        <i style="cursor:pointer" class="fa fa-close" data-dismiss="toast"></i>
      </div>
      <div class="toast-body">
        {@html item.message}
      </div>
    </div>


    {/each}


  </div>
</div>